# NTOU Computer Networking FinalTerm Notes

> [!NOTE]  
> 線上版本，請參閱：[https://hackmd.io/@whitebear13579/rkw20SEefl](https://hackmd.io/@whitebear13579/rkw20SEefl)
> 實際期末考試範圍請以當年度教授公告為準，若有疑義請洽詢授課教師。

AI 生的，還沒改

---

## 📘 Chapter 1 — Introduction（緒論）

### 1. 什麼是 Internet？
**兩種觀點：**
- **Nuts and bolts view（組件觀點）**：由 hosts（end systems）、communication links、packet switches（routers / switches）組成；ISP 互連形成「網路的網路」。
- **Service view（服務觀點）**：提供基礎建設給應用程式（Web、email、streaming…）；提供 programming interface（socket API）給分散式應用。

**重要名詞：**
- **Protocol**：定義通訊實體（entities）之間訊息交換的**格式、順序**，以及訊息傳送/接收時採取的**動作**。
- **RFC** (Request for Comments) — 由 **IETF** 制定 Internet 標準。

### 2. Network Edge（網路邊緣）
- **Hosts**：clients & servers
- **Access networks**：
  - **DSL / Cable (HFC)**：FDM 共享，asymmetric（下行 > 上行）
  - **Home network**：cable/DSL modem + router + WiFi AP
  - **Enterprise / Institutional**：Ethernet (1/10 Gbps)
  - **Wireless**：WiFi 802.11 (11/54/450 Mbps)；4G/5G cellular
- **Physical media**：
  - Guided：twisted pair、coaxial cable、fiber optic
  - Unguided：terrestrial radio、satellite radio

### 3. Network Core（網路核心）
**Packet Switching vs Circuit Switching（必考！）**

| 特性 | Circuit Switching | Packet Switching |
|---|---|---|
| 資源分配 | 預先保留（dedicated） | 隨需使用（on-demand） |
| 連線建立 | 需要 call setup | 不需要 |
| 多工方式 | FDM、TDM | 統計多工（statistical multiplexing） |
| 適用 | 傳統電話 | 突發性資料、Internet |
| 浪費 | idle 時資源閒置 | 可能 congestion |

**Store-and-Forward**：整個 packet 必須先完整抵達 router 才能向下一個 link 傳送。
- 一個 hop 的傳送延遲 = L/R（L=packet bits, R=link bandwidth）
- N 個 link 的 end-to-end 延遲 ≈ N · L/R（不含 propagation delay）

### 4. Internet Structure（網路結構）
- **Tier-1 ISPs**（如 AT&T、NTT、Level 3）位於頂層，全球互連
- **IXP**（Internet Exchange Point）：多個 ISP 互連點
- **Content provider networks**（如 Google、Facebook）：自有網路直連 access ISP，常 bypass tier-1

### 5. Performance（效能）— 四種延遲（必考！）
**節點總延遲 d_nodal = d_proc + d_queue + d_trans + d_prop**

| 延遲 | 說明 |
|---|---|
| **Processing delay** | router 檢查 header、判斷輸出 port（µs 級） |
| **Queueing delay** | 在 buffer 等待傳送（依擁塞程度而定） |
| **Transmission delay** | L/R（packet 推入 link 的時間） |
| **Propagation delay** | d/s（在 link 上傳播的時間） |

**Traffic intensity** = La/R （a=arrival rate, L=packet length, R=bandwidth）
- La/R → 0：queueing delay 小
- La/R → 1：queueing delay 大
- La/R > 1：buffer 滿，丟封包

**Throughput**：bits/sec 在 sender → receiver 的速率
- end-to-end throughput = **min(R_s, R_c, R/N)**（bottleneck link 決定）

### 6. Security（資安）
- **Malware**：virus（需 user 執行）、worm（被動執行）、spyware、botnet
- **DoS / DDoS** 攻擊：bandwidth flooding、connection flooding、vulnerability attack
- **Packet sniffing**：被動竊聽 LAN/WiFi 上的封包
- **IP spoofing**：偽造 source IP

### 7. Protocol Layers（協定層次）— **必考！**
**Internet 5-layer Stack：**
| 層次 | 功能 | 範例 |
|---|---|---|
| **Application** | 支援網路應用 | HTTP, SMTP, IMAP, DNS |
| **Transport** | process-to-process 資料傳輸 | TCP, UDP |
| **Network** | 從來源到目的之 datagram 路由 | IP, routing protocols |
| **Link** | 相鄰節點間資料傳輸 | Ethernet, 802.11 WiFi, PPP |
| **Physical** | bits "on the wire" | — |

**OSI 7-layer**：Internet 多了 **Presentation**（加密、壓縮）與 **Session**（同步、檢查點）兩層。

**Encapsulation（封裝）**：
- Application 訊息 M → Transport 加 H_t 變 segment → Network 加 H_n 變 datagram → Link 加 H_l 變 frame → Physical 傳 bits
- Router 處理到 Network 層；Switch 處理到 Link 層

---

## 📗 Chapter 2 — Application Layer（應用層）

### 1. 應用程式架構
- **Client-Server**：永遠在線的 server，固定 IP，data center；clients 不直接互通
- **P2P**：peers 互相通訊；self-scalable；管理複雜（IP 變動）
- **Hybrid**：兩者混合

### 2. 應用層服務需求
| 應用 | 資料遺失 | 吞吐量 | 時間敏感 |
|---|---|---|---|
| File transfer / e-mail | 不可遺失 | elastic | No |
| Web | 不可遺失 | elastic | No |
| Streaming audio/video | 容忍少量 | 需要 | Yes (秒級) |
| Interactive games | 容忍少量 | 少量 | Yes (10s ms) |

### 3. Transport 服務
- **TCP**：可靠、流量控制、擁塞控制、連線導向；**不提供**：timing、最低吞吐保證、安全
- **UDP**：不可靠資料傳輸；**不提供**：連線、可靠性、流量/擁塞控制
- **TLS**：在 TCP 之上提供加密、資料完整性、端點驗證（應用層實作）

### 4. Web 與 HTTP
**HTTP 特性**：
- 使用 TCP（port 80），**stateless**（伺服器不維護過去請求的狀態）

**Non-persistent vs Persistent HTTP：**
- **Non-persistent (HTTP/1.0)**：每個物件需要新的 TCP 連線；每個物件 = 2 RTT + 傳輸時間
- **Persistent (HTTP/1.1)**：多個物件共用一條 TCP 連線；可 pipeline

**HTTP 訊息類型**：GET, POST, HEAD, PUT, DELETE
**回應狀態碼**：200 OK、301 Moved Permanently、400 Bad Request、404 Not Found、505 HTTP Version Not Supported

**Cookies（維持狀態）**：4 components — Set-Cookie header、Cookie header、Cookie file、後端 DB

**Web Cache（Proxy Server）**：
- 減少 response time、減少 access link 流量
- **Conditional GET**：使用 `If-modified-since` header；若未修改回 `304 Not Modified`

**HTTP/2 → HTTP/3**：
- HTTP/2：單一 TCP 連線、object prioritization、push
- HTTP/3：以 **UDP+QUIC** 為基礎，加上安全性、per-object 錯誤與擁塞控制

### 5. E-mail
**三大元件**：User agents、Mail servers、SMTP
- **SMTP** (Simple Mail Transfer Protocol, RFC 5321)
  - 使用 TCP（port 25）
  - **Push** 協定（HTTP 是 pull）
  - 限制 ASCII 7-bit；`CRLF.CRLF` 結束訊息
  - persistent connections
- **IMAP** (RFC 3501)：訊息存於 server，提供 retrieve、刪除、folder
- HTTP 介面：Gmail / Yahoo Mail 等

### 6. DNS — **必考！**
**服務**：hostname-to-IP translation、host aliasing、mail server aliasing、load distribution

**為何不集中？** Single point of failure、流量大、距離遠、維護困難 → **分散式階層架構**

**DNS 階層**：
1. **Root DNS servers**（13 個 logical servers，全球複製）
2. **TLD servers**（.com、.org、.edu、國別域名）
3. **Authoritative servers**（組織自己的 DNS）
4. **Local DNS server**（每個 ISP 都有，cache 結果）

**兩種查詢**：
- **Iterative**：被查詢者回答「去問另一個 server」
- **Recursive**：被查詢者代替發問者去解析

**DNS 記錄類型（RR format: name, value, type, ttl）**：
| Type | name | value |
|---|---|---|
| **A** | hostname | IP address |
| **NS** | domain | authoritative server hostname |
| **CNAME** | alias | canonical name |
| **MX** | name | mail server name |

**DNS Caching**：local DNS server 快取結果，TTL 過期才刪除（root 很少被訪問）

**DNS Security**：DDoS attacks、redirect attacks、DNS poisoning、利用 DNS 進行 DDoS 放大攻擊；DNSSEC 提供安全

### 7. P2P & BitTorrent
- **Peer churn**：peer 隨時加入/離開
- **BitTorrent**：tracker 追蹤 peers；torrent 內檔案切成 chunks；rarest first；tit-for-tat（top 4 chunkers 取得 unchoked，每 30s optimistically unchoke 一個 peer）

### 8. Video Streaming & CDN
- **DASH (Dynamic Adaptive Streaming over HTTP)**：影片分成不同位元率的 chunks，client 動態選擇
- **CDN**：分散式 server 分布全球；用 DNS 重導向使用者到最近 server
- **OTT challenges**：從哪個 CDN 抓？network congestion 時怎麼辦？

### 9. Socket Programming
- **UDP socket**：`socket(AF_INET, SOCK_DGRAM)`；無連線；用 `sendto` / `recvfrom`
- **TCP socket**：`socket(AF_INET, SOCK_STREAM)`；server `bind`→`listen`→`accept`；client `connect`

---

## 📙 Chapter 3 — Transport Layer（傳輸層）

### 1. 傳輸層服務
- 提供 **process-to-process** 邏輯通訊（network 層提供 host-to-host）
- **Multiplexing/Demultiplexing**：
  - **UDP demux**：僅靠 **dest port #**（不同 source IP/port 都送到同一 socket）
  - **TCP demux**：使用 **4-tuple**（source IP, source port, dest IP, dest port）

### 2. UDP — User Datagram Protocol
- **特性**：no frills、best effort、可能 lost / out-of-order；no connection、no congestion control
- **優點**：no setup（無 RTT 延遲）、簡單、small header、可在擁塞時繼續傳
- **應用**：DNS、SNMP、HTTP/3、串流影音
- **UDP segment**：source port | dest port | length | checksum | payload
- **Checksum**：將 segment 內容（含 pseudo header）視為 16-bit integers，做 **one's complement sum**；接收端計算後比對

### 3. 可靠資料傳輸原理（RDT）
**漸進式設計：**
- **rdt 1.0**：完全可靠 channel — sender just sends
- **rdt 2.0**：channel 有 bit errors → 加 checksum + ACK/NAK
- **rdt 2.1**：ACK/NAK 也可能 corrupted → 加 **sequence numbers** (0/1)
- **rdt 2.2**：用 duplicate ACK 取代 NAK
- **rdt 3.0**：channel 也會 lose packets → 加 **timer**，timeout 重傳

**Stop-and-Wait 效率：U_sender = (L/R) / (RTT + L/R)** — 在高頻寬下效率極低

### 4. Pipelined Protocols
**Go-Back-N (GBN)**：
- Sender：window size N，accumulative ACK，timer for oldest unACKed
- Timeout → 重傳所有未 ACK 封包
- Receiver：丟棄 out-of-order，只送 cumulative ACK

**Selective Repeat (SR)**：
- Sender 個別管理每個封包的 timer
- Receiver buffer out-of-order 封包，個別 ACK
- 注意：sender / receiver window 不同步可能造成問題（sequence # space ≥ 2N）

### 5. TCP — **必考重點！**
**特性**：point-to-point、reliable in-order byte stream、pipelined、full duplex、connection-oriented、flow controlled

**TCP segment 結構**：
- source port | dest port | sequence # | ACK # | head len | flags (CEUAPRSF) | rwnd | checksum | urg pointer | options | data

**Sequence numbers & ACKs**：
- Seq # = byte stream "number" of first byte in segment's data
- ACK # = seq # of **next byte expected** from other side（cumulative ACK）

**RTT 估計**：
```
EstimatedRTT = (1-α)·EstimatedRTT + α·SampleRTT   (α=0.125)
DevRTT = (1-β)·DevRTT + β·|SampleRTT - EstimatedRTT|   (β=0.25)
TimeoutInterval = EstimatedRTT + 4·DevRTT
```

**Fast Retransmit**：收到 **3 duplicate ACKs** → 不等 timeout 立即重傳

**TCP Flow Control**：
- Receiver 通告 `rwnd`（free buffer space）
- Sender 限制 LastByteSent − LastByteAcked ≤ rwnd

**TCP 連線管理 — 3-way Handshake**：
1. Client → Server：SYN=1, seq=x
2. Server → Client：SYN=1, ACK=1, seq=y, ACKnum=x+1
3. Client → Server：SYN=0, ACK=1, ACKnum=y+1（可帶資料）

**關閉**：FIN exchange（雙方各送 FIN，並 ACK 對方的 FIN）

### 6. Congestion Control
**Causes/Costs of Congestion**：
- Throughput 達上限後不再增加
- 大量 queueing delay
- 不必要的重傳（造成資源浪費）
- 上游 router 的工作被下游丟棄（wasted work）

**兩種途徑**：
- **End-to-end**：TCP 用（從 loss / delay 推測擁塞）
- **Network-assisted**：router 直接通知（如 ECN）

### 7. TCP Congestion Control — **必考！**
**AIMD（Additive Increase, Multiplicative Decrease）**：
- 每個 RTT 增加 1 MSS（線性）
- 偵測到 loss（3 dup ACK）→ cwnd 減半
- Timeout → cwnd 設為 1 MSS（TCP Tahoe）

**三大狀態**：
| 狀態 | 行為 | 進入下一狀態 |
|---|---|---|
| **Slow Start** | 每個 ACK → cwnd += 1 MSS（指數成長） | cwnd ≥ ssthresh → CA |
| **Congestion Avoidance** | 每個 ACK → cwnd += MSS·(MSS/cwnd)（線性） | 3 dup ACK → Fast Recovery |
| **Fast Recovery** | 每個 dup ACK → cwnd += MSS | new ACK → CA |

**Loss event 處理**：
- **3 dup ACK**：ssthresh = cwnd/2、cwnd = ssthresh + 3
- **Timeout**：ssthresh = cwnd/2、cwnd = 1 MSS、回 Slow Start

**TCP 吞吐量近似公式**：`avg throughput ≈ 0.75 · W / RTT`（W = max window size）

**TCP CUBIC**：用三次函數逼近 Wmax，Linux 預設

**Delay-based congestion control（BBR）**：保持 pipe "just full enough"，避免高延遲

### 8. QUIC（HTTP/3）
- 應用層協定，跑在 UDP 上
- **單一 handshake** 同時建立連線 + 安全（TCP+TLS 需要 2 個）
- 解決 HTTP/2 的 HOL blocking（每個 stream 獨立 RDT）

---

## 📕 Chapter 4 — Network Layer: Data Plane（網路層 — 資料平面）

### 1. 網路層概觀
**兩大功能**：
- **Forwarding（轉發）**：將封包從 router 的輸入 port 移到適當輸出 port（**data plane**，硬體，奈秒級）
- **Routing（路由）**：決定封包從來源到目的的路徑（**control plane**，軟體，毫秒級）

**Two control plane approaches**：
- **Per-router control**（傳統）：每個 router 跑 routing algorithm
- **SDN**：邏輯集中式 controller 計算後安裝 forwarding table

### 2. Router 架構
**輸入埠功能**：line termination → link layer protocol → lookup/forwarding/queueing → switch fabric
- **Match + Action**：使用 header 欄位值查表，再執行動作
- **Destination-based forwarding**（傳統）：只看 dest IP
- **Generalized forwarding**：看任意 header 欄位

**Longest Prefix Matching**：當有多個 prefix 都 match 時，選**最長**的那個。常以 **TCAM** 實作（一個 clock cycle 完成）。

**Switching fabric** 三種：
- Memory switching、Bus switching、Crossbar switching

**Input Port Queueing**：HOL blocking（隊首阻塞）— 隊首 datagram 阻擋後面的
**Output Port Queueing**：buffer overflow → drop policy；scheduling discipline 決定哪個先送

**Buffering 規則**：
- RFC 3439：average buffering = typical RTT × C
- 較新建議：with N flows, buffering = (RTT · C) / √N

### 3. IP (Internet Protocol)
**IPv4 Datagram 格式**：
- ver | head len | TOS | total length | identifier | flags | frag offset | TTL | upper layer | header checksum | source IP | dest IP | options | data
- 標頭固定 20 bytes（無 options）

**IPv4 Addressing**：
- 32-bit identifier，**dotted-decimal**（如 223.1.1.1）
- 與 **interface** 綁定（host 1-2 個 interface；router 多個）
- **Subnet**：device interface 不經 router 即可互通
  - subnet part：高位元（共同前綴）
  - host part：低位元

**CIDR (Classless InterDomain Routing)**：a.b.c.d/x，x = subnet 位元數

**IP Address 取得方式**：
- 整個 subnet 從 ISP 取得位址空間
- **Host 取得 IP**：手動設定 / **DHCP** 動態取得
- **DHCP** 流程：DHCP Discover → DHCP Offer → DHCP Request → DHCP ACK（**DORA**）
  - DHCP 還回傳：first-hop router、DNS server、subnet mask

**NAT (Network Address Translation)**：
- 私有位址範圍：10/8、172.16/12、192.168/16
- NAT router 改寫 (source IP, port) ↔ (NAT IP, new port)，維護 translation table
- **優點**：節省 IP、隱藏內網、可換 ISP；**缺點**：違反 end-to-end principle、阻礙 P2P

**IP Fragmentation/Reassembly**：
- 大於 MTU 的 datagram 被切片
- 用 ID、flag、offset 欄位識別
- **只在目的端重組**
- offset 以 8 bytes 為單位計算

### 4. IPv6
**動機**：32-bit 位址空間耗盡、加速處理、支援 flow

**IPv6 格式**：固定 **40 bytes** header
- ver | priority | flow label | payload len | next hdr | hop limit | 128-bit source | 128-bit dest

**與 IPv4 的差異**：
- 128-bit 位址、no checksum、no fragmentation/reassembly（由 sender 處理）、no options（移到 next header）

**過渡技術 — Tunneling**：IPv6 datagram 作為 IPv4 datagram 的 payload

### 5. Generalized Forwarding & SDN
**OpenFlow Flow Table**：
- Match：12 個欄位（Ingress Port、Src/Dst MAC、Eth Type、VLAN、IP Src/Dst、IP Proto、IP TOS、TCP/UDP src/dst port）
- Action：forward / drop / modify / encapsulate to controller
- Stats：packet & byte counters

**OpenFlow 統一抽象**：
- Router = longest prefix match + forward
- Switch = MAC dst + forward/flood
- Firewall = IP+port + permit/deny
- NAT = IP+port + rewrite

### 6. Middleboxes
- 從專屬硬體 → 白盒硬體 + 開放 API
- **SDN**：邏輯集中式控制與配置
- **NFV (Network Functions Virtualization)**：在白盒上跑可程式化的網路功能

---

## 📓 Chapter 5 — Network Layer: Control Plane（網路層 — 控制平面）

### 1. Routing 概觀
**Routing 與 Forwarding 關係**：Routing 決定路徑 → 產生 Forwarding table → 由 Forwarding 執行

**Graph 抽象**：N (nodes) + E (edges)，每條 edge 有 cost；目標：找最短路徑

### 2. Link State Routing — Dijkstra's Algorithm
**特性**：
- **Centralized**：每個 node 都有完整 topology 與所有 link cost（透過 **link state broadcast**）
- Iterative：k 次迭代後得到 k 個目的的最短路徑

**演算法**：
```
1. Initialization: N' = {u}, for all v: D(v) = c(u,v) if neighbor else ∞
2. Loop: find w not in N' with min D(w); add w to N'
   update D(v) = min(D(v), D(w) + c(w,v)) for v adjacent to w
3. Until all nodes in N'
```

**複雜度**：O(n²)（每次迭代 O(n)，n 次迭代）；可優化至 O(n log n)

**Message complexity**：O(n²) link crossings（每個 router broadcast 給 n routers）

**Oscillation problem**：當 link cost 依賴 traffic 時，可能發生 routing oscillation

### 3. Distance Vector Routing — Bellman-Ford
**Bellman-Ford equation**：
```
Dx(y) = min_v { c(x,v) + Dv(y) }
```

**特性**：
- **Distributed、Asynchronous、Iterative、Self-stopping**
- 每個 node 只跟**鄰居**交換 distance vector
- DV 改變才通知鄰居

**Link cost changes**：
- 「**Good news travels fast**」：cost 下降快速擴散
- 「**Bad news travels slow**」：cost 上升可能造成 **count-to-infinity** 問題
- 解決方法：**Poisoned Reverse**（告訴鄰居：「如果我經過你到 dest，那 dest 對我是 ∞」）

### 4. LS vs DV 比較（必考！）

| 比較項目 | Link State | Distance Vector |
|---|---|---|
| Message 複雜度 | O(nE)，每次變動需 broadcast | 只通知鄰居，但可能多次迭代 |
| 收斂速度 | O(n²)，較快 | 不一定，可能 count-to-infinity |
| 強健性（router 故障） | 只通告自己的 link | 錯誤值會被傳染 |
| 知識範圍 | 全網拓樸 | 只知到鄰居的距離 |

### 5. Intra-AS Routing：OSPF
**AS (Autonomous System)**：管理上同一個 organization 的 routers

**OSPF (Open Shortest Path First)**：
- **Link-state** routing，**Dijkstra** 算法
- 直接跑在 IP 上（不用 TCP/UDP）
- 多種 cost metrics（bandwidth、delay…）
- 訊息有 authentication
- **Hierarchical OSPF**：兩層 — Local Area + Backbone
  - Area border routers、Backbone routers、Boundary routers

**其他 intra-AS protocols**：RIP（DV，已棄用）、EIGRP（Cisco，2013 開放）、IS-IS

### 6. Inter-AS Routing：BGP
**BGP (Border Gateway Protocol)**：Internet "膠水"，de facto inter-domain protocol

**兩種 session**：
- **eBGP**：取得鄰近 AS 的可達性資訊
- **iBGP**：把資訊散布到自己 AS 內所有 routers

**BGP path attributes**：
- **AS-PATH**：經過的 AS 序列
- **NEXT-HOP**：下一跳 router 介面

**BGP route selection**（優先順序）：
1. **Local preference**（policy decision）
2. **Shortest AS-PATH**
3. **Closest NEXT-HOP router**（hot potato routing — 燙手山芋路由：把封包儘快丟給最近的 gateway，不管 inter-domain cost）
4. 額外條件

**Why different Intra- / Inter-AS routing?**
- **Policy**：inter-AS 需要 admin 控制流量；intra-AS 沒此問題
- **Scale**：hierarchical routing 降低 table size
- **Performance**：intra-AS 重效能；inter-AS 重 policy

### 7. SDN (Software Defined Networking)
**四個關鍵特性**：
1. Flow-based forwarding（如 OpenFlow）
2. **Data plane / Control plane 分離**
3. Control plane 功能移到 data-plane switch 之外
4. Programmable control applications

**SDN Controller**（network OS）：
- **Southbound API**：與 switches 溝通（如 OpenFlow）
- **Northbound API**：給控制應用程式
- 通常實作為分散式系統（容錯）

**OpenFlow protocol**：跑在 TCP 上（可加密）
- **Controller-to-switch**：features、configure、modify-state、read-state、send-packet
- **Asynchronous (switch → controller)**：packet-in、flow-removed、port-status
- **Symmetric**：hello、echo

### 8. ICMP (Internet Control Message Protocol)
- 用於 host / router 通報網路層資訊
- 在 IP 「之上」（封裝在 IP datagram 內）
- 訊息格式：type、code + 造成錯誤的 IP datagram 前 8 bytes

**常見 type/code**：
| Type | Code | 說明 |
|---|---|---|
| 0 | 0 | echo reply (ping) |
| 3 | 0/1/2/3 | dest unreachable (network/host/protocol/port) |
| 8 | 0 | echo request (ping) |
| 11 | 0 | TTL expired |
| 12 | 0 | bad IP header |

**Traceroute 原理**：送出一系列 TTL=1, 2, 3... 的封包，每個 router 在 TTL 變 0 時回 ICMP "TTL expired"

### 9. Network Management
- **SNMP** (Simple Network Management Protocol)：傳統，使用 PDU 取得/設定 MIB 物件
- **NETCONF/YANG**：較新，actively 配置整個網路、用 YANG 描述資料模型

---

## 📔 Chapter 6 — Link Layer & LANs（連結層）

### 1. 連結層服務
- **Framing**：將 datagram 封裝為 frame，加 header & trailer
- **Link access**：MAC protocol 解決多重存取
- **Reliable delivery**（低錯誤率連結通常不需要；無線連結常需要）
- **Flow control**、**Error detection**、**Error correction**
- **Half-duplex / Full-duplex**

**實作**：在 **NIC**（network interface card）或晶片上實作

### 2. 錯誤偵測與更正
**Parity Check**：
- Single bit parity：偵測 1 bit error
- 2D parity：偵測**並更正** 1 bit error

**Internet Checksum**：用於 TCP/UDP/IP；簡單但保護較弱

**CRC (Cyclic Redundancy Check)** — **必考**：
- 強大的錯誤偵測編碼
- D = d 個資料 bits；G = r+1 個生成多項式 bits
- 選 r 個 CRC bits R，使得 <D,R> 對 G 整除（mod 2）
- 接收端用 G 除 <D,R>，餘數非 0 → 偵測到錯誤
- 可偵測所有小於 r+1 bits 的 burst errors
- 廣泛用於 Ethernet、802.11 WiFi

### 3. 多重存取協定（MAC Protocols）— **必考！**
**三大類別**：

#### A. Channel Partitioning（通道分割）
- **TDMA**：時間切成 slot，每個 station 固定 slot；unused slots 浪費
- **FDMA**：頻率分割，每個 station 固定 frequency band

#### B. Random Access（隨機存取）
- **ALOHA (pure)**：要傳就傳；frame 在 [t₀−1, t₀+1] 內可能碰撞；效率 **~18%**
- **Slotted ALOHA**：同步 slot；只在 slot 開頭傳；效率 **1/e ≈ 37%**
- **CSMA (Carrier Sense Multiple Access)**：先聽再傳；仍可能碰撞（傳播延遲）
- **CSMA/CD**：偵測到碰撞立刻中止；**Ethernet** 使用
  - Binary exponential backoff：第 m 次碰撞 → 從 {0,1,...,2^m −1} 選 K，等 K·512 bit times
  - 效率 = 1 / (1 + 5·t_prop / t_trans)
- **CSMA/CA**：碰撞避免，**WiFi** 使用（無線不易偵測碰撞）

#### C. Taking Turns（輪流使用）
- **Polling**：主節點輪流邀請每個 slave 傳送
- **Token passing**：token 在 ring 上傳遞，誰有 token 誰能傳

### 4. LAN — 連結層位址
**MAC Addresses**：
- 48-bit 位址（如 1A-2F-BB-76-09-AD）
- IEEE 統一管理（製造商買位址空間）
- **Flat** address：可攜（移到別的 LAN 仍可用）
- IP address：階層式，**不可攜**（依附於 subnet）
- 廣播位址：**FF-FF-FF-FF-FF-FF**

### 5. ARP (Address Resolution Protocol)
**問題**：知道 IP，怎麼知道 MAC？

**ARP table**：每個節點維護 <IP, MAC, TTL>（TTL 約 20 分鐘）

**流程（同 subnet）**：
1. A 廣播 ARP query（dest MAC = FF-FF-FF-FF-FF-FF）詢問 B 的 MAC
2. B 收到後回 unicast ARP reply
3. A 把 B 的 (IP, MAC) 加進自己的 ARP table

**跨 subnet**：
- 送 datagram 給 first-hop router（用 ARP 查 router 的 MAC）
- Router 解 frame，查 IP 路由表，**重新封裝**新的 frame（dst MAC 換成下一跳的 MAC）

### 6. Ethernet
**主流的有線 LAN 技術**

**Frame 結構**：
- preamble (8 bytes：7×10101010 + 10101011，同步時脈) | dest MAC (6) | src MAC (6) | type (2) | data (46-1500) | CRC (4)

**特性**：
- **Connectionless**：無 handshake
- **Unreliable**：無 ACK / NAK
- MAC：unslotted CSMA/CD + binary backoff
- 多種速率：10 Mbps、100 Mbps、1 / 10 / 40 Gbps；通用 MAC + frame format

**Topology**：bus（90 年代主流）→ switched（目前主流）

### 7. Switches（交換器）
**特性**：
- Link-layer device，**store-and-forward** Ethernet frames
- 檢查 MAC，選擇性 forward 到對應 link
- **Transparent**：host 不知有 switch
- **Plug-and-play、self-learning**：不需配置

**Switch table**：<MAC address, interface, TTL>

**Self-learning**：
- 收到 frame 時，記錄 **source MAC ↔ incoming interface**
- 找不到 dst MAC → **flood**（除來源 port 外全部送出）
- 找到 → 只送到該 interface（若是來源 port 則 drop）

**Switch vs Router 比較（必考）**：

| 比較項目 | Switch | Router |
|---|---|---|
| 層次 | Link layer (L2) | Network layer (L3) |
| 檢查 | Link-layer header | Network-layer header |
| 表格 | self-learning（flooding） | routing algorithm（IP） |
| Plug-and-play | ✅ | ❌ |
| Scale | LAN 規模有限（broadcast） | 階層式路由可擴展 |

### 8. VLAN (Virtual LAN)
**Port-based VLAN**：在單一實體 switch 上以 port 分組成多個 virtual LAN
- **Traffic isolation**：不同 VLAN 互不互通
- **Dynamic membership**：可動態指派 port
- 跨 VLAN 通訊：透過 router

**跨多個 switch 的 VLAN**：
- **Trunk port** 攜帶多個 VLAN 的 frames
- **802.1Q frame format**：在原 Ethernet frame 中插入 4 bytes（2-byte Tag Protocol Identifier 0x8100 + 2-byte Tag Control Information，含 12-bit VLAN ID + 3-bit priority）

### 9. MPLS (Multiprotocol Label Switching)
- 用 **fixed-length label** 取代最長前綴比對 → 高速 forwarding
- 借用 Virtual Circuit 概念
- IP datagram **仍保留 IP 位址**
- MPLS header 插在 link 與 network layer 之間（"shim header"）
- Label 結構：label (20) | Exp (3) | S (1) | TTL (5) bits

### 10. Data Center Networking
- 10,000s ~ 100,000s host、密集互連
- **Top-of-Rack (TOR) switch** → tier-2 switch → tier-1 switch
- **Rich interconnection**：多路徑提供冗餘與負載平衡
- **Application-layer load balancing**：將請求分發給多個 server

### 11. A Day in the Life of a Web Request
**綜合 protocol stack 流程**（連到 google.com）：

1. **連線** → 透過 **DHCP** 取得 IP、router、DNS server（DHCP→UDP→IP→Eth；目的 MAC 為 broadcast FF...）
2. **DNS 查詢** → 但要先用 **ARP** 取得 first-hop router 的 MAC
3. **ARP** → 廣播查詢 router MAC，router 回 reply
4. **DNS 查詢** → 找 google.com 的 IP（local DNS server 可能也要遞迴）
5. **TCP 3-way handshake** → SYN → SYNACK → ACK
6. **HTTP GET** → server 回 HTTP response
7. **網頁顯示**

---

## 🎯 應考重點提醒

### 必背公式
- Stop-and-wait utilization：**U = (L/R) / (RTT + L/R)**
- Throughput bottleneck：**min(R_s, R_c, R/N)**
- TimeoutInterval = EstimatedRTT + 4·DevRTT
- TCP avg throughput ≈ 0.75·W / RTT
- Slotted ALOHA 最大效率 = 1/e ≈ **37%**
- Pure ALOHA 最大效率 = **18%**
- CSMA/CD 效率 = 1 / (1 + 5·t_prop/t_trans)

### 重要對照表

**Demultiplexing**：
- UDP：dest port
- TCP：(src IP, src port, dst IP, dst port) 4-tuple

**主要 protocol & layer**：
- Application：HTTP / SMTP / IMAP / DNS / FTP
- Transport：TCP / UDP / QUIC
- Network：IP / ICMP / OSPF / BGP
- Link：Ethernet / WiFi / PPP / ARP / DHCP

**注意 DHCP 與 ARP**：
- **DHCP** 是 application layer protocol（跑在 UDP 上），但提供 network layer 資訊
- **ARP** 在 IP 與 link 之間（嚴格說屬於 link layer 的輔助）

### 計算題常見類型
1. **延遲計算**：傳輸延遲、傳播延遲、queueing
2. **Subnet 規劃**：CIDR 切割、可用 host 數（2^n − 2）
3. **TCP congestion control**：給 cwnd vs round 圖，判斷 SS / CA / FR、ssthresh
4. **Dijkstra 演算法**：給拓樸，畫表
5. **Distance vector**：幾輪後 DV 表
6. **CRC**：給 D 與 G，算 R
7. **Longest prefix match**：給 routing table 與 dest IP，問走哪個 interface