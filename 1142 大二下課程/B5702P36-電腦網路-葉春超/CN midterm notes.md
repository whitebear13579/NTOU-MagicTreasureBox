# NTOU Computer Networking Midterm Notes

> [!NOTE]  
> 線上版本，請參閱：[https://hackmd.io/@whitebear13579/BkNB6RrKbx](https://hackmd.io/@whitebear13579/BkNB6RrKbx)
> 實際考試範圍請以當年度教授公告為準，若有疑義請洽詢授課教師。

## Quickly Tables
### Chapter 1
- [協定 Protocol](#協定-Protocol)
- [網路結構 Internet Structure]()
- !!重要!! [封包交換 Packet Switching](#封包交換-Packet-Switching)
- [電路交換 Circuit Switching](#電路交換-Circuit-Switching)
- [轉送與路由 Forwarding and Routing](#轉送與路由-Forwarding-and-Routing)
- [封包遺失與延遲 Packet Loss and Delay](#封包遺失與延遲-Packet-Loss-and-Delay)
- [吞吐量 Throughput](#吞吐量-Throughput)
- [安全性 Security](#安全性-Security)
- !!重要!! [網際網路協定堆疊](#網際網路協定堆疊-Network-Protocol-Layers)

### Chapter 2
- !!重要!! [網路應用程式 Network Application](#網路應用程式-Network-Application)
- [Web and HTTP](#Web-and-HTTP)
- !!重要!! [網域名稱系統 The Domain Name System (DNS)](#網域名稱系統-The-Domain-Name-System-(DNS))
- [對等式應用程式架構 Peer-to-Peer Application Architecture](#對等式應用程式架構-Peer-to-Peer-Application-Architecture)
- !!重要!! [串流與內容分發網路 Streaming and Content Distribution Networks (CDNs)](#串流與內容分發網路-Streaming-and-Content-Distribution-Networks-(CDNs))

## Chapter 1
### 協定 Protocol
協定是**規範網路實體間如何溝通的一種規則**。在協定中，規範了這些東西：
- `message format` : 訊息的格式長甚麼樣子
- `message order` : 什麼先送？什麼後送？
- `actions` : 訊息送出（或收到）後，該執行什麼動作？

### 網路結構 Internet Structure
- **Network Edge:** hosts、clients、server、data center
> 指能夠連接上網際網路的各種裝置媒介。

- **Access Network:** wired、wireless communication links
> 將網路邊緣的實體連上網路的第一個路由器
> 需要特別注意傳輸速率（transmission rate） 與資源的狀態（共享還是私有？）

- **Network Core**
> 由無數個路由器（節點）組成。（簡報上叫做網路の網路）
> 負責封包的轉送。

### 封包交換 Packet Switching

<div style="text-align:center;">

![image](https://hackmd.io/_uploads/H1P2UkIYbx.png =70%x)

</div>

在封包交換下，發送端會將在應用層（application layer）的長篇訊息分割成較小的資料塊，這些資料塊就是封包（packets）。

封包交換的特性：
#### 儲存並轉發 （Store-and-forward）
每個路由器需要接收到完整的封包，才會將其轉送（forward）到下一個鏈路上。

傳輸延遲（Transmission Delay）：$d_{trans} = \frac{L}{R}$
- $d_{trans}$：路由器把封包整個丟上鏈路的傳輸時間（秒）。
- $L$：封包長度（bit）。
- $R$：鏈路頻寬（bps）。

這個公式代表著，如果你想傳送一個 $L$ bit 的封包到速率為 $R$ bps 的鏈路上，需要耗費多少秒。
而 router在執行 「儲存並轉發」時，並不會收到前幾個bit就立即開始轉送。
通常會先接收完整個封包之後，才會開始推到下一條鏈路上。
所以，顯而易見的，如果一個封包可能在不只一個鏈路上流通時，其傳輸延遲就會越大。
比如，封包需要經過兩個鏈路，那就會須要 $2 \frac{L}{R}$ 的時間。

#### 依需求分配、資源共享（On-demand allocation & Resource sharing）
與 Circuit Switching 不同要求專享資源不同，封包交換允許多使用者共享同一鏈路資源，只有在通訊真正發生時才會使用到網路頻寬。
- 不需要像電路交換在傳輸前進行連線建立（no call setup）。
- 封包交換所產生的問題：
    - 佇列延遲 Queueing Delay：如果鏈路上正忙於其他封包，新到的封包就必須在緩衝區（Buffer）中等待。
    - 封包丟失 Packet Loss：若路由器中的緩衝區滿了，新到的封包就會被捨棄。

### 電路交換 Circuit Switching

<div style="text-align:center;">

![image](https://hackmd.io/_uploads/rJE-ARrtWe.png =50%x)

</div>

在電路交換網路中，當兩台裝置想要通訊時，系統會為整個連線鏈路預留專屬的資源（頻寬），整個通訊過程中，這個資源完全由該連線獨佔，即使通訊處於閒置狀態時亦是如此。

為了讓多個使用者共享同一物理鏈路，衍伸出了 FDM 與 TDM 兩種技術

#### 分頻多工 Frequency Division Multiplexing / FDM

<div style="text-align:center;">

![image](https://hackmd.io/_uploads/Bkk2xJ8Ybg.png =70%x)
    
</div>

將**鏈路中的頻譜劃分為多個狹窄的頻段（frequency bands）**，每次通訊裝置都會被分配到一個專屬的頻段，通訊期間裝置依然始終專享此頻段，以該頻段的最大速率進行通訊。

#### 分時多工 Time Division Multiplexing / TDM

<div style="text-align:center;">

![image](https://hackmd.io/_uploads/HJvReyUt-l.png =70%x)
    
</div>

將**傳輸時間劃分為固定的時間槽（Time Slots）**，類似 OS 中 Time Sharing 機制，裝置每一次通訊會被週期性的分配到專屬的時間槽，每個裝置在通訊時可以在屬於自己的時間槽中使用完整的頻段，以最大速率傳輸資料。

| 特性     | FDM                    | TDM                |
| -------- | ---------------------- | ------------------ |
| 分配方式 | 切分頻率               | 切分時間           |
| 資源使用 | 持續占用部分頻寬       | 週期性占用完整頻寬 |
| 硬體需求 | 需使用濾波器來區分頻率 | 需要精確的時間同步 |

#### 封包交換與電路交換的比較
| 特性     | 電路交換 Circuit Switching       | 封包交換 Packet Switching              |
| -------- | -------------------------------- | -------------------------------------- |
| 資源分配 | 資源獨佔，頻寬固定           | 資源共享，動態分配                     |
| 連線建立 | 需要事先建立連線（Call Setup）   | 不須事先建立連線                       |
| 傳輸效率 | 即使沒傳輸資料，頻寬也會被占用 | 利用率高，非常適合突發性（Bursty）流量 |
| 穩定性     | 穩定、延遲固定，適合傳統電話     | 可能會有擁塞延遲或丟包                 |

### 轉送與路由 Forwarding and Routing
轉送（Forwarding） 與路由（Routing）是網路中的兩個關鍵功能。

#### 轉送 Forwarding
- 定義：當一封包抵達路由器輸入點（Input Link）時，路由器必須將封包移動到適合輸出點（Output Link）。
> 決定現在這個輸入進來的封包要走哪個門出去。

- 動作範圍：局域（local），單一路由器內。
- 所屬平面：資料平面（Data Plane）

路由器內維護著一張轉發表（Forwarding Table），當封包進入路由器時，路由器會檢查封包標頭（Header）中的目的地，並根據轉發表決定封包該從哪個 Port 出去。

**轉發表（Forwarding Table）是在路由（Routing）階段被生成。**


#### 路由 Routing
- 定義：決定封包從起點（Source）到終點（Destination）所經過的鏈路要怎麼走。
> 導航，決定路怎麼走（但不一定每次出來的結果都是好的）

- 動作範圍：全域（global），影響整個傳輸路徑。
- 所屬平面：控制平面（Control Plane）

路由器根據路由演算法（類似圖論，會利用到 Dijkstra、Bellman-Ford）來決定。路由器之間彼此也會交換訊息，來了解整個網路的拓撲結構。

**Forwarding 中所用到的轉發表（Forwarding Table）就是根據每個路由計算結果所生成的。**

|          | 轉發 Forwarding                                  | 路由 Routing                                             |
| -------- | ------------------------------------------------ | -------------------------------------------------------- |
| 動作範圍 | 局域（local），單一路由器內。                    | 全域（global），影響整個傳輸路徑。                       |
| 所屬平面 | 資料平面（Data Plane）                           | 控制平面（Control Plane）                                |
| 花費時間 | 極快（由硬體實作，或軟體上由字典樹、哈希表實現） | 較慢（經資訊交換取得拓撲結構後，還要透過演算法決定路徑） |
| 主要任務 | 將封包從輸入端移動到輸出端                       | 計算出最佳路徑並維護轉發表                               |

### 封包遺失與延遲 Packet Loss and Delay
當封包抵達路由器的速度暫時超過鏈路容量時，封包就會在路由器中的緩衝區（buffer）排隊等待。
若路由器的緩衝區滿了，新來的封包就會被捨棄，這就是丟包（Packet Loss）

<div style="text-align:center;">

![image](https://hackmd.io/_uploads/SJsq4rxq-g.png =50%x)

</div>

一個節點的延遲被記為 $d_{nodal}$ 

<div style="text-align:center; font-size:20px">

$d_{nodal} = d_{proc} + d_{queue} + d_{trans} + d_{prop}$

</div>

而他的結果是由以下 4 種延遲加總而來：

#### $d_{proc}$ 處理延遲 Processing delay
當封包進入 router 之後，需要先對其做基本處理，例如檢查 bit error、查看 header 之類的，這段時間就是 processing delay，通常很小。

#### $d_{queue}$ 等待延遲 Queueing delay
如果現在很多封包都想走同一個 output link 出去，後抵達的封包就需要排隊等待，這段等待的時間就是 queueing delay，其花費時間和壅塞程度有關。

#### $d_{trans}$ 傳輸延遲 Transmission delay
把整個封包推到鏈路上所需要的時間即是所謂的 transmission delay。就是剛剛在[儲存並轉發](#儲存並轉發-（Store-and-forward）)提到到的公式 $d_{trans} = \frac{L}{R}$
在計算傳輸延遲中，我們只關心兩件事：**封包有多大？**、**鏈路速率有多快？**
> 把一整列火車開進隧道，從第一節到最後一節完全開進去所需要的時間。
> [name=人類科技結晶 aka 矽基生命體]

#### $d_{prop}$ 傳播延遲 Propagation delay
傳播延遲與傳輸延遲的不同點是，傳播延遲是 **這整個封包在鏈路上跑所花的時間**，而傳輸延遲是 **路由器把封包推上鏈路所花的時間**。
在計算傳播延遲中，我們只關心兩件事：**距離多遠？**、**訊號跑多快？**
> 一列火車從隧道入口開到隧道出口所需要的時間。
> [name=人類科技結晶 aka 矽基生命體]

傳播延遲的計算公式為 $d_{prop} = \frac{d}{s}$，其中：
- $d_{prop}$：封包在整段鏈路上從起點至終點的所耗時間（秒）。
- $d$：鏈路長度。
- $s$：訊號傳播的速度。

:::warning
特別注意到，$d_{nodal}$ 只是**單一節點的延遲**，並**不是整條路徑的總延遲**。
端對端延遲（end-to-end delay）是計算經過許多路由，鏈路後的延遲，這才是整條路徑的總延遲。

$\rightarrow$ 也就是說，若封包一路上經過許多的節點，每個節點都會有自己的 $d_{nodal}$
:::

### 吞吐量 Throughput

<div style="text-align:center;">

![image](https://hackmd.io/_uploads/HkywULlqWx.png =70%x)

</div>

吞吐量指單位時間內，位元從發送端到接收端的速率，而吞吐量又分為瞬時吞吐量（instantaneous throughput）與平均吞吐量（average throughput）

#### 瓶頸鏈結 Bottleneck link
吞吐量的大小取決於封包路徑上頻段最小的那段鏈路（像關鍵反應速率那樣）。
- $R_s$：伺服器端傳輸速率。
- $R_c$：接收端鏈路速率。

當 $R_s < R_c$ 時，此時吞吐量為 $R_s$。
<div style="text-align:center;">

![image](https://hackmd.io/_uploads/B1nyuIg9Wl.png)

</div>

當 $R_s > R_c$ 時，此時吞吐量為 $R_c$。
<div style="text-align:center;">

![image](https://hackmd.io/_uploads/SytxdUxcZl.png)
    
</div>

### 安全性 Security
網際網路的設計初衷是由一群互相信任的使用者互相建立連線，使資料的傳輸更便利。
因此，原始協定並未考慮到安全性的問題，也缺乏身分驗證與加密。

#### 惡意軟體 Malware
大致可以分為以下幾類：
- 殭屍網路 Botnet：攻擊者控制大量受感染主機，並利用這些主機發動大規模攻擊。
- 病毒 Virus：一種自我複製的程式，通常需要透過「人為動作」才能傳播（例如開啟惡意郵件附件）。
- 蠕蟲 Worm：一種自我複製的程式，但其能主動偵測網路中的安全漏洞並自動傳播，不需要人為干預。
- 間諜軟體 Spyware：能夠記錄使用者的鍵盤輸入、造訪網站等行為，並傳回給收集者。

#### 阻斷服務攻擊 Denial of Service
網路中最常見的攻擊之一，目的是讓正常的網路資源無法被使用。
通常利用規模龐大的殭屍網路對目標同時發出海量請求，耗盡目標的頻寬或處理資源。

#### 嗅探與欺騙 Sniffing & Spoofing
- 封包嗅探 Packet Sniffing
    - 在 Wifi 或乙太網路等廣播式的共享媒介中，攻擊者可以監視所有流經的封包。
    - 若傳輸內容沒有被加密，攻擊者便能夠讀取你的密碼、訊息等敏感資料。
- IP 欺騙 IP Spoofing：
    - 攻擊者在發送封包時，偽造標頭中的來源 IP，用以偽造身分、或讓目標主機在收到攻擊封包後，回覆給錯誤的受害者主機。


### 網際網路協定堆疊 Network Protocol Layers
依照所使用的課本，目前網際網路協定的標準架構共分為 5 層，由上而下：
- **應用層 Application Layer**
    - **支援各類網路應用程式**
    - 常見的協定： DNS、SMTP、HTTP
    - 資料單位：Message
- **傳輸層 Transport Layer**
    - **負責 Process 與 Process 間的資料傳輸**
    - 常見的協定： TCP（可靠傳輸、流量控制）、UDP（不可靠、傳輸快速）
    - 資料單位：Segment
- **網路層 Network Layer**
    - **負責將資料報從來源地址轉送到目標地址**
    - 常見的協定： IP、各種路由協定（Rounting Protocols）
    - 資料單位：Datagram
- **連結層 Link Layer**
    - **負責相鄰網路設備之間的資料傳輸**
    - 常見的協定： WiFi、Ethernet
    - 資料單位：Frame。
- **物理層 Physical Layer**
    - **負責在物理媒介上傳送位元（Bits）、處理實際的電信號**

#### 封裝 Encapsulation

<div style="text-align:center;">

![image](https://hackmd.io/_uploads/BJx_AUU2Zx.png =70%x)

欲傳送的 Message 在 Application Layer 產生
$\downarrow$
在 Transport Layer 加入 Header，變成 Segment $H_t$
*包含端口資料以及封包序號，確保資料能送到對的 Process*
$\downarrow$
在 Network Layer 加入 Header，變成區段 Datagram $H_n$
*包含 IP 位址與 TTL，確保資料能送到對的主機*
$\downarrow$
在 Link Layer 加入 Header，變成 Frame $H_I$ 
*包含 Mac 位址，將資料轉送到下一個相鄰節點*
$\downarrow$
從 Physical Layer 傳出

</div>

## Chapter 2
### 網路應用程式 Network Application
#### 應用程式架構 Application Architecture
- 主從式架構（Client-Server）
    - Server 永遠在線，具有永久且固定的 IP 位址。
- 對等式架構（Peer-Peer）
    - 沒有永遠在線的中央伺服器，且可能為動態 IP。
    - 任意端（節點）直接進行通訊。
    - 自我擴展性（Self-ScalaBility）：新的節點（Peer）加入雖然增加了整體服務的需求，但同時也能貢獻服務能力（如上傳頻寬）

#### Process 間的通訊 Process Communication
- Process 位於同一台主機
    - Process 間透過作業系統的 IPC 機制（[詳見 OS 筆記](https://hackmd.io/@whitebear13579/r13qofFJZl#IPC-in-Shared-memory--Message-Passing-System)）進行通訊。
- Process 位於不同主機
    - Process 間透過 Application Layer 的協定進行 Message 的交換。
    - client process 為發起通訊的一方，server process 為被動等待通訊的一方。
    - P2P 架構下，一個應用程式同時扮演 client process 與 server process。

#### Socket & Process 定址
- Socket
    - Process 間透過網路收發資料的門戶被稱為 Socket。
    - 被視為 Application Layer 與 Transport Layer 間的 API。
- Process 定址
    - 為了正確的接收 message，Process 必須要有一個標示符（Identifier）來辨別。
    - 標示符中包含 IP 位址（辨別主機）與 Port（辨別Process）。

#### 應用程式對傳輸服務的需求 Application Requirements for Transport Services
不同應用程式對網路傳輸有著不同的需求，主要分為四個面向：
- **資料完整性 Data Intergrity**
    - 檔案傳輸、Web 需要保證 100% 的資料完整性。
    - 音訊、影片串流則可以容許些微的資料損失。
- **吞吐量 Throughput**
    - 某些應用（如視訊會議、影音串流）需要有最低頻寬的保證，才能流暢運作。
    - 也有些應用（如電子郵件）對頻寬的需求則相對較低。
- **時效性 Timing**
    - 如網路電話、線上遊戲等即時應用，對延遲的要求極高。
- **安全性 Security**
    - 資料加密、身分驗證、完整性檢查......

#### TCP vs UDP
- TCP（Transmission Control Protocol）
    - 可靠傳輸 reliable transport：提供可靠的資料傳輸，能確保資料完整性。
    - 流量控制 flow control：資料發送速度根據接收端動態調整，保證丟包率。
    - 壅塞控制 congestion control：當頻寬過載時動態調整傳輸速率。
    - 連線導向 connection-oriented：在傳輸資料前需要先建立連線。
    - 不提供延遲與頻寬保證。
- UDP（User Datagram Protocol）
    - 不可靠傳輸 unreliable transport：不保證資料完整性，可能會有丟包、重複、亂序等問題。
    - 無須在傳輸前建立連線。
    - 不提供流量控制、壅塞控制、延遲與頻寬保證、不保證資料完整性。
    - $\rightarrow$ 是劣勢也是優勢：沒有建立連線前的延遲，也沒有壅塞控制，想發多快就發多快，輕量快速。

### Web and HTTP
#### HTTP Overview
HTTP（HyperText Transfer Protocol）是 Web 在 Application Layer 的基礎協定，
- 主從式架構：
    - client (browser)：請求、接收並顯示 web 物件。
    - sever：接受請求並回傳對應的物件。
- HTTP 在 Transport Layer 中使用 TCP，預設 Port 為 80。
- 無狀態性 stateless：server 不會保存 client 端先前的任何請求資訊。這是設計上的一種簡化，但也導致需要引入如 cookie 之類的額外機制來維持"狀態"。

:::info
**為什麼 HTTP 不維護狀態？**
要求協定維護狀態是件很複雜的事，會增加協定的實作難度與運行成本。
這意味著需要把過去的歷史資料的保存起來 $\rightarrow$ 增加儲存的成本。
此外，當 server 或 client 其中一方出現問題時，雙方的狀態可能會不同步，導致錯誤的發生。
:::

#### 非持續與持續連線 Non-persistent vs Persistent Connections
HTTP 的連線分為兩種狀態，一種是非持續連線，另一種是則是持續連線。
- 非持續連線 HTTP 
    - 每個 TCP 連線只傳送一個物件，傳完後立即關閉。因此，在使用非持續性 HTTP 時，若要下載多個物件，就需要多次建立 HTTP 連線。
    - HTTP 下載一個物件時會需要用到 **2 個 RTT**（一個用於 TCP 握手、另一個用於 HTTP 請求 + 響應） + 檔案傳輸的時間。
    - *RTT：Round Trip Time，指一個封包從發送端到接收端，然後再回到發送端所需的時間，是一種衡量網路延遲的指標。
- 持續連線 HTTP
    - 伺服器在發送響應後保持 TCP 連線開啟，後續的請求與響應可以在同一連線上完成。
    - 除了發送第一個物件前需要 TCP 握手外，後續的物件理論上只需要一個 RTT。
    - HTTP 1.1 以後的預設行為。

#### HTTP 訊息格式 HTTP Message Format
HTTP 分為兩種訊息：request 與 response。
<div style="text-align:center;">

![image](https://hackmd.io/_uploads/rJo_x9IhZl.png =70%x)

</div>

- Request Message

<div style="text-align:center;">

![image](https://hackmd.io/_uploads/H1L5zqI2be.png =70%x)

</div>

- 請求行 Request Line：請求方法（GET、POST、HEAD、PUT、DELETE）、URL、協定版本等資訊。
- 標頭行 Header Lines：Host、User-Agent、Accept-Language 等資訊。
- 實體主體 Entity Body：與 Header Lines 之間有一行空白行。是實際要傳送給伺服器的資料。
- Respone Message

<div style="text-align:center;">

![image](https://hackmd.io/_uploads/BJU2M9L2-x.png =70%x)

</div>

- 狀態行 Status Line：協定版本、狀態碼等資訊。
- 標頭行 Header Lines：提供伺服器與物件的額外資訊。
- 實際回傳資料與 request message 一樣，跟 header lines 之間有一行空白行。

:::info
**常見的 HTTP 狀態碼**
- 200 OK：請求成功。
- 301 Moved Permanently：所請求的物件已被移動。新的位置會在訊息後的 Location 欄位中提供。
- 400 Bad Request：請求訊息格式錯誤。
- 404 Not Found：伺服器找不到請求的物件。
- 500 Internal Server Error：伺服器內部發生錯誤。
- 505 HTTP Version Not Supported
:::

#### Cookies
由於 HTTP 是無狀態的，為了維護伺服器與使用者的狀態，HTTP 引入了 Cookie 的機制。網站與 client 的瀏覽器之間使用 cookies 來維護在不同 transaction 之間的狀態。
cookies 由以下 4 個部分組成：
- HTTP 響應訊息中的 cookie header line（Set-cookie）
- 下一個 HTTP 請求訊息中的 cookie header line（Cookie）
- 儲存在使用者主機的 cookie file（由 browser 管理）
- 網站後端的資料庫（記錄 Cookie ID 與使用者行為的對應關係）
$\rightarrow$ cookies 的存在讓 HTTP 訊息可以攜帶狀態資訊，並在多次的 transaction 之間於傳送端與接收端維持狀態。

#### Web Caching
為降低 Origin Server 負載與減少頻寬的使用，Web 快取希望在不透過 Origin Server 的情況下滿足使用者的請求，同時還能加快 client 端請求的響應時間。

<div style="text-align:center;">

![image](https://hackmd.io/_uploads/Hy3sQoL3Zx.png =60%x)

</div>

- HOW TO IMPLEMENTS? $\rightarrow$ 瀏覽器將所有請求送到代理伺服器（Proxy Server）
    - 如果 Proxy Server 中有快取物件，直接回傳給 client。
    - 如果 Proxy Server 中沒有快取物件，則向 Origin Server 發出請求，將回傳的物件快取起來，再回傳給 client。

#### 條件式取得 Conditional GET
如果 Origin Server 上的物件被修改了，但 Proxy Server 中的快取仍然是舊的，使用者便有可能拿到過期的資料。
為了解決快取內容過期的問題，HTTP 引入了條件式取得（Conditional GET）的機制。

<div style="text-align:center;">

![image](https://hackmd.io/_uploads/B1PMPiU2bl.png =50%x)

</div>

- HOW TO IMPLEMENTS? $\rightarrow$ 當 Proxy Server 發送請求時加入特定的標頭（`If-modified-since: <date>`）。
    - 如果未修改：Origin 回傳 304 Not Modified 且不包含任何物件。
    - 如果已修改：Origin 回傳 200 OK 給 Proxy 並帶上新的物件內容。

#### HTTP 的改進 HTTP's Improvements
- **HTTP 1.1**：引入持續連線與管線化，但存在嚴重的 HOL Blocking 問題。
- **HTTP 2.0**：引入框架化交錯傳輸與優先順序控制，在 Applcation Layer 中解決 HOL Blocking 問題。
- **HTTP 3.0**：內建安全機制、強化錯誤控制、解決 Transport Layer 的 HOL Blocking 問題；並改用 UDP 取代 TCP 來實現更靈活的傳輸控制

*\*HOL Blocking（Head-of-Line Blocking）*：1.1 中雖然引入了管線化處理，但由於 Server 依然必須 FCFS 的回應請求，因此若第一個請求的回應需要較長時間，後續的請求就會被阻塞，導致整體效能下降。 


### 網域名稱系統 The Domain Name System (DNS)
#### DNS 的基本功能 DNS Basic Functionality
- 主機名稱到 IP 位址的轉換：DNS 主要任務是將人類易讀的域名（如 www.google.com）轉換為機器可識別的 IP 位址（如 140.121.99.151）。
- 主機別名：一個主機可以有一個規範名稱（Canonical Name）和多個別名（Alias Name）。
- 負載平衡：一個域名可以對應到多個 IP 位址（伺服器叢集），DNS 可以根據一些機制來分配請求回傳不同 IP，達到平衡負載的效果。

#### DNS 的分散式階層架構 DNS Distributed Hierarchical Architecture
DNS 採用分散式、階層式的資料庫架構，透過一系列的 DNS Server來完成域名解析的任務。

<div style="text-align:center;">

![image](https://hackmd.io/_uploads/SJcSRFDhWe.png)

</div>

- **根域名伺服器 Root Name Servers**
    - 當底下的 DNS Server 都無法解析某個域名時，最後求助的對象。
    - 由 ICANN 進行維護管理，全球有 13 個邏輯上的根域名伺服器，以 A~M 編號，並用任播在全世界各地設立多個鏡像站。
- **頂級域名伺服器 Top-Level Domain (TLD) Servers**
    - 負責 `.com`、`.org`、`.net`、`.edu`、`.aero`、`.jobs`、`.museums`，以及所有國碼頂級網域（如：`.jp`、`.tw`、`.us`、`.fr`...）的域名解析。
- **權威域名伺服器 Authoritative DNS Servers**：
    - 由組織（可能是大學或公司）自己管理的 DNS 伺服器，提供該組織已命名主機的真實 IP 映射。
    - 可能由該組織維護，或是由 ISP 負責維護。
- **本地域名伺服器 Locaal DNS Severs**：
    - 不屬於 DNS 階層式架構中，但極為重要。
    - 每個主機發出 DNS 查詢時第一個接觸的 DNS 伺服器。
    - 可以充當快取 DNS 伺服器，暫時保存之前查詢過的域名與 IP 映射，但資料可能會過期。
    - 負責將查詢送往 DNS 階層中。

:::info
**DNS 為甚麼不採用中心化的設計？**
中心化的設計會導致許多問題，比如說單點故障（Single Point Failure）導致可用性降低、抑或是資料量龐大導致維護困難。
此外，若 DNS 是以中心化的方式設計的話，這代表單一伺服器需要承擔來自全世界的查詢請求，負載量龐大。
使用者若距離中心化的 DNS 伺服器較遠，也會產生較高的延遲。
:::

#### Iterative vs Recursive DNS query

當主機 `engineering.nyu.edu` 想要知道 `gaia.cs.umass.edu` 的 IP 位址時：
<div style="text-align:center">

Iterative Query　　　　　　　　　　　Recursive Query
![](https://hackmd.io/_uploads/BJQLF9Pnbl.png =45%x) ![image](https://hackmd.io/_uploads/B1A8F5w3bx.png =47%x)

</div>

- **疊代查詢 Iterative Query**：如果被詢問的 DNS Server 不知道答案，就回傳"下一層該問誰"的位址，由 local DNS Server 來繼續詢問下一層的 DNS Server，直到找到答案為止。這是預設的查詢方式。
- **遞迴查詢 Recursive Query**：將本地 DNS 的負擔完全交給上一層，由上一層去問完結果後再回傳。這麼做會導致越上層的 DNS Server 負擔越重。

#### DNS Caching
為了提升查詢效率，並減輕上層 DNS Server 的負擔，DNS 引入了快取機制。
當某個 DNS Server 查詢到了一個映射關係後，其會先將這個關係記錄在記憶體中一段時間，並設定一個 TTL（Time To Live）值，當 TTL 到期後，這個快取的資料就會被丟棄。

:::info
如果依照 DNS 的查詢方式來看，可以發現我們每一次的請求都可能會需要經過許多的 DNS Server 才能得到最終的 IP 位址。
但實務上，我們一次的查詢只需要經過部分的 DNS Server 就能得到答案，這是因為 local name servers 通常會快取 TLD Server 的位址，因此實務上很少真正的去訪問道 Root name server。
:::

#### DNS 記錄 DNS Records
我們將一筆 DNS 資源紀錄（Resource Records）簡稱為 RR，RR 的格式為 `<name, value, type, ttl>`。
其中，常見的記錄格式（type）有以下幾種：
- **A**
    - `name` 是 主機名稱，`value` 是對應的 IPv4 位址。
    - IPv6 使用 **AAAA** 記錄。
- **NS**
    - `name` 是一個域名，`value` 是該域名的權威 DNS 伺服器的主機名稱。
- **CNAME**
    - `name` 是一個別名（Alias），`value` 是規範名稱（Canonical）。
    - `name` 和 `value` 都是域名，其作用在將一個域名別名指向一個規範名稱（暱稱對應到本人）。
    - 比如說：www.ibm.com 是 servereast.backup2.ibm.com 的別名。
- **MX**
    - 用於電子郵件系統中的記錄。
    - `value` 是與 `name` 相關聯的郵件交換伺服器的主機名稱。

#### DNS 的回應訊息 DNS Reply Message
DNS 的查詢（query）與回覆（reply）訊息都有著相同的格式：

<div style="text-align:center;">

![image](https://hackmd.io/_uploads/S15SXswhWx.png =60%x)

</div>

- 訊息標頭 message header：
    - identification：16 位的查詢編號、回覆與查詢使用相同的編號。
    - flags：
        - 是查詢（query）還是回覆（reply）
        - 是否要求遞迴查詢
        - 伺服器是否支援遞迴
        - 是否是具授權的回覆（來自 Authoritative DNS Servers ）
- 查詢 / 回應資訊：
    - Questions：查詢中的名稱與類型欄位
    - Answer：該查詢所對應的資源紀錄
    - Authority：權威伺服器的記錄
    - Additional info：額外資訊

### 對等式應用程式架構 Peer-to-Peer Application Architecture
#### P2P 架構的特性 P2P Architecture Characteristics
- 去中心化：沒有永遠在線的中央伺服器。
- 端點直接通訊：任意的一對主機（Peers）可以直接互相通訊。
- 自我擴展性 (Self-scalability)：這是最重要的優點。新的 Peer 加入雖然增加了服務需求，但也同時貢獻了服務能力（提供上傳頻寬給其他 Peer）。
- 間歇性連接：Peers 可能會隨時加入或離開網路，且 IP 位址經常變動（稱為 Churn，流失率）。

#### 檔案分發的時間：Client-Server vs P2P
假設從一台伺服器分發大小為 $F$ 的檔案給 $N$ 個使用者

<div style="text-align:center;">

![image](https://hackmd.io/_uploads/SJSJciP2Ze.png =60%x)

</div>

- 主從式架構 Client-Server：
    - 伺服器必須發送 $N$ 份檔案副本，受限於伺服器上傳頻寬 $u_s$
    - 隨著使用者 $N$ 增加，分發時間會呈線性成長
    - 因此，在此架構下所耗的時間為 $$D_{c-s} \le max(NF/u_s, F/d_{min})$$
    - $NF/u_s$：發送 $N$ 份大小為 $F$ 的檔案所花的時間
    - $d_{min}$：所有 client 中下載速度最慢的那個
- 對等式架構 Peer-to-Peer：
    - 伺服器只需要發送一份副本，剩下的由使用者（Peer）之間彼此互傳
    - 隨使用者 $N$ 增加，雖然需求隨之上升，不過總上傳頻寬也會隨之上升
    - 分發時間成長極為緩慢，在大規模分發下遠快於主從式架構。
    - 因此，在此架構下所耗的時間為 $$D_{P2P} = \max\{ F/u_s, F/d_{\min}, NF / (u_s + \sum u_i) \}$$
    - $u_s + \sum u_i$：整個系統中的總上傳速率（伺服器的上傳速率 $u_s$ 加上共 $i$ 個 Peer 可以貢獻的上傳速率 $\sum u_i$ ）。
    - $NF / (u_s + \sum u_i)$ ：整個系統的上傳能力瓶頸。

#### BitTorrent
BitTorrent 是 P2P 最成功的案例之一。
- Chunks：每個檔案會被切分為許多大小固定的小塊，通常為 256KB。
- Trackers：一個中央節點，負責記錄哪些 Peer 目前正在參與這個 Torrent，並告訴新加入的 Peer 應該去聯繫誰。
- 塊的請求策略：
    - 罕見優先 Rarest-first：優先請求在鄰居中擁有數量最少的 Chunk，確保稀有的 Chunk 不會消失。
- 塊的發送策略：
    - 疏遠 Choking：Peer 會限制上傳頻寬，只給目前給自己傳輸速率最快的前 4 個鄰居。
    - 隨機嘗試 Optimistic Unchoking：每隔 30 秒會隨機選一個新的鄰居傳送資料。目的是發現是否有潛在的"好鄰居"能提供更快的速率。

### 串流與內容分發網路 Streaming and Content Distribution Networks (CDNs)
#### 多媒體影音的基礎 Fundamentals of Multimedia
在串流技術中，最重要的是將影音資料以節省網路頻寬。
- 影像編碼（Encoding）：利用影像內的冗餘來減少資料量。
    - 具體來說，如果影像是一個連貫運動過程的話，我們可以大致將資料分為移動的物體與不移動的背景。
    - 對於移動中像素點，我們可以紀錄下他移動的相對方向，只記錄下某幾個關鍵幀，而不是每幀都存，這便是一種最簡單的想法。簡報上提了兩種大致的方法：空間編碼和時間編碼。
    - 空間編碼 (Spatial coding)：利用單一幀影像內的冗餘（例如背景是同一種顏色，只需記錄顏色和重複次數）。
    - 時間編碼 (Temporal coding)：利用連續幀之間的冗餘（例如只記錄第 $i+1$ 幀與第 $i$ 幀之間的差異部分）。
- 位元率 Bit Rate
    - CBR (Constant Bit Rate)：固定的影片編碼速率。
    - VBR (Variable Bit Rate)：編碼速率隨影像內容複雜度（空間與時間編碼量的變化）而動態改變 。
#### 串流資料的緩衝 Buffering for Streaming Data
伺服器到客戶端之間的頻寬可能會隨時間波動（網路擁塞），且封包遺失或延遲會導致影像播放中斷或畫質下降。
為了解決這個問題，得在播放端引入額外緩衝機制。
- 播放端緩衝 Playout Buffering
    - 主要實現：播放端（client）在收到資料後不會立即播放，而是先存入緩衝區。並根據緩衝區狀態與網路頻寬來動態決定要緩衝的資料量。
    - 這些緩衝資料被用來補償網路產生的抖動延遲（Delay Jitter）。只要緩衝區內還有資料，網路暫時的頻寬下降就不會影響到播放的順暢度。

#### 動態自適應串流 Dynamic, Adaptive Streaming over HTTP (DASH)
透過 HTTP 協定來實現動態且自適應的串流，是現代 OTT 影音平台最關鍵的技術之一。
- 伺服器端：將影片檔切分成多個小區塊，每個區塊都以不同的位元率（不同畫質）進行編碼並儲存，並提供客戶端 manifest file（含 chunk 請求地址），告知其有哪些位元率可以選擇。
- 客戶端：客戶端主動判斷何時來請求下一個區塊，並根據網路頻寬狀態自動選擇合適的畫質，以及決定去哪一台伺服器請求資源。

$\rightarrow$ **串流影片 = 影像編碼 + 播放緩衝 + DASH**

#### 內容分發網路 Content Delivery Network (CDNs)
前面提過，仰賴單一伺服器可能會導致許多問題，比如單點故障、網路擁塞、長距離延遲、負載過高等問題。
為此，引入了**內容分發網路**（Content Delivery Network、Content Distribution Network，簡稱 CDN）來解決這些問題。
CDN 的策略，是在全球範圍內部署許多快取伺服器，讓使用者能夠就近取得其請求的資源，over HTTP 的特性也能很好的複用現有的網路基礎設施。
- CDN 的部署策略可以分為幾種：
    - **Enter Deep**：將 CDN 伺服器深入佈署到各地的存取網路中，使其能夠靠近使用者
    - **Bring Home**：將較小但規模更大的伺服器叢集，放在接近使用者的接入結點（POPs / Point of Presnece）中。

當使用者請求資源時，CDN 會透過 DNS 重新導向將使用者的請求導向至地理位置較近或目前頻寬最充裕的副本節點 。

#### IPTV 與 OTT
OTT（Over The Top）是相對於傳統有線電視（IPTV）的全新媒體串流形式。

| 比較內容 | IPTV                                   | OTT                                  |
| -------- | -------------------------------------- | ------------------------------------ |
| 網路環境 | 由電信商提供的專用網路                 | 現有公共網際網路                     |
| 服務品質 | 專用頻寬、能提供幾乎不卡頓的高畫質影像 | 畫質與體驗取決於使用者當下的網路環境 |
| 觀看設備 | 需要額外的機上盒進行訊號解碼           | 任何能存取公共網際網路的設備         |
| 傳輸技術 | 多採用多播 （Multicast）               | 任播（Unicast） + DASH + CDN         |
| 代表例子 | 中華電信 MOD                           | 巴哈姆特動畫瘋            |


## Chapter 3
老子不讀了，操
https://hackmd.io/@bob840806/r1Q5GaMUX <- 這個會考
還考了 TCP：使用的 Application layer protocol（HTTP）、header 最小大小（20 bytes）、Acknowledgement Number 的意義、Receive Window 為 0 的處理方式，以及 header 與 data 的邊界判斷（Data Offset 欄位）。
封包序列追蹤（A0 A1 B0 A1 B_A1 C0 C1 C0 這樣的順序）， pipeline 協定下重傳的行為。

