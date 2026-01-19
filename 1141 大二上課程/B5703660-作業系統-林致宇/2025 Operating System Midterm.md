# 2025 Operating System Mideterm

# 3A OS 期中考大補帖
- 線上版本，請參閱：https://hackmd.io/@whitebear13579/r13qofFJZl。
- 實際考試範圍與內容請依教授公告與課本內容為主。

## 考前重點
- Chapter 1:
    - [Context Switch、Interrupt（名詞定義、解釋）](#Context-Switch-上下文切換-)
    - [SMP / NUMA 架構](#SMP--NUMA-架構)
    - [Time Sharing（名詞定義、解釋）](#Time-Sharing--Multitasking--分時系統-)
- Chapter 2:
    - [Inter-Process Communication (IPC)](#Inter-Process-Communication-IPC-程序間的通訊)
    - [Mechanism & Policy](#Policy-amp-Mechanism-作業系統的政策與機制)
    - [Microkernals](#Microkernals-微核心架構-理想化)
- Chapter 3:
    - [Process](#Process-程序-)
    - [Layout of a Process in memory](#Layout-of-a-Process-in-Memoery-程序在記憶體中的結構)
    - [Program State Diagram (不會考圖但圖很重要)](#Program-State-Diagram-狀態圖，很重要)
    - [Process Control Block (PCB)](l#Process-Control-Block-PCB-程序控制區塊)
    - [Schedulers](#Schedulers)
    - [Process Termination (Zombie / Orphan Process)](#Process-Termination-Zombie--Orphan-process-肯定會考，看一下)
    - [IPC in Shared memory / Message Passing System](#IPC-in-Shared-memory--Message-Passing-System)
- Chapter 4:
    - [Thread](#Thread-線程-)
    - [Multicore Programming](#Multicore-Programming-多核心程式設計)
    - [Amdal's Law](#Amdals-Law-阿爾達姆定律)
    - [MultiThreading Models](#MultiThreading-Models-多執行續模型8)
- Chapter 5:
    - [CPU IO Burst Cycle](#CPU-IO-Burst-Cycle)
    - [Preemptive / Non-preemptive Scheduling](#Preemptive--Non-Preemptive-Scheduling-搶佔式與非搶佔式排程)
    - [Scheduling Criteria](#Scheduling-Criteria-評量一個排程演算法的好壞)
    - [FCFS / SJF](#Scheduling-Algorithm-FCFS--SJF-必考)
    - [ RR (有 $q$ 的那個，會考畫甘特圖、算 avg wating time)](#Scheduling-Algorithm-Round-Robin)
    - [Multi-Processor Scheduling](#Multi-Processor-Scheduling)
    - [Real-Time CPU Scheduling](#Real-Time-CPU-Scheduling)
    - [Algorithm Evalution](#Algorithm-Evalution)
## Chapter 1
### Context Switch 上下文切換 ?
Context Switch 是 OS 在切換 Process 的一個過程，切換 Process 時，OS 會先將舊 Process 的狀態存入 PCB ( 行程控制區塊 / Process Control Block )，接著載入新的 Process 執行，這就是所謂的 Context Switch。

### Interrupt 中斷 ?
Interrupt 是由硬體或軟體發送給 CPU 的事件。發生 Interrupt 時，會連帶觸發 Context Switch。CPU 會先將目前的狀態(reg / mem 的值)儲存起來，然後先去處理 Interrupt，當 Interrupt 處理完之後，再進行一次 Context Switch，把剛剛存的狀態載入回來。

### SMP / NUMA 架構
- SMP : SMP 架構下，多個 Processer 會同時共享一套主記憶體與 IO 裝置，每個Processer 都是獨立運作的，在同個 OS 下同時執行。
- NUMA :  NUMA 架構的出現是為了解決 SMP 架構下的記憶體競爭問題，為了降低資料傳輸流的附載，將系統設計成多個節點，每個節點下都有自己的 Processer 與記憶體，節點之間仰賴系統互連 ( system interconnect ) 來溝通。

>　SMP 是多顆 CPU 共享單一記憶體、可做所有工作的架構；NUMA 為減少瓶頸，把 CPU 分成多節點、各有本地記憶體，跨節點存取較慢。

### Time Sharing ( Multitasking ) 分時系統 ?
CPU 會在多個工作之間來回高頻的切換，讓使用者能夠在 OS 上同時執行多個應用程式。 Time Sharing 的 Response Time 通常小於 $1$ 秒。

## Chapter 2
開始進入第二章前，先熟悉一些簡單的 System Call：
- fork : 新建一個 Process
- kill : 終止一個 Process

### Inter-Process Communication (IPC) 程序間的通訊
不同 Process 間為交換資料與協調行為而使用的機制，有兩大模型：
- Shared Mem. Modal (共享記憶體模型)
    - 兩 Process 間使用共同的資源，可以互相交換資訊。
    - 會產生**同步問題**。
- Message-Passing Modal (訊息傳遞模型)
    - 類似 Network 間的訊息交換策略。
    - 以 OS 提供的 send / recive interface 做訊息交換
    - 不共享記憶體空間，沒有同步問題，較前者安全
    - 較慢。
   
記得去看 Chapter 3 的 [IPC in Shared memory / Message Passing System](#IPC-in-Shared-memory--Message-Passing-System)

### Policy & Mechanism 作業系統的政策與機制
一個在作業系統的設計中非常重要的原則：機制 ( Mechanism ) 與策略 ( Policy ) 分離。   
- 策略 Policy : 要 **做什麼？** / 要 **什麼時候做** 
    - 類似一種演算法或是規則
    - 策略調整時**不能夠改動到底層的機制**。
    - 比如說，Context Switch 下一個 Process 要執行誰？
- 機制 Mechanism : 要 **怎麼做？** (底層怎麼實現)
    - 底層方法、工具該怎麼實現？
    - 要夠**彈性**，能**支援多種策略**。
    - 比如說，要有 Context Switch 機制才能實現 Time Sharing？
> Policy 決定做什麼與何時做；Mechanism 負責如何實作；兩者分離讓系統更彈性、可擴充。

### Microkernals 微核心架構 (理想化)
Policy / Mechanism 的極致：將核心簡化到只剩下基本功能，盡可能地把其他的服務移到使用者空間中(user space)，不同的模組之間以訊息傳遞(message passing)互通。   
- Advantage:
    - 易於擴充、維護
    - 易於移植至新硬體架構
    - 更可靠、安全
- Disadvantage:
    - user space 與 kernal space 之間的通訊會帶來額外開銷
    - 做到極致的 MicroKernals 仍有難處，現**多數 OS 不是採完全 MicroKernals 設計**
> 現今大部分作業系統採整體式架構，同時搭配動態載入 (LKMs) 模組的設計 

## Chapter 3
### Process 程序 ?
- 擁有**獨立的位址空間與資源**
- Thread 算是他的孩子
- 創建成本高、涉及 Context Switch -> Heavy Weight
- 執行單執行緒程式時，只會有一個 PC
- Program 是一個存放在硬碟中的**被動實體**，而 Process 是存在於記憶體中的**主動實體**
- Program 被執行，放入記憶體後，就會變成 Process
- 一個程式可以有多個 Process。
### Layout of a Process in Memoery 程序在記憶體中的結構
![image](https://hackmd.io/_uploads/S1M0AkckWl.png)   
- Stack Section : 儲存呼叫函式時所需的暫存資料，包含回傳位址、函式參數、區域變數。
- Heap Section : 這邊的記憶體是程式在 Run Time 時動態分配的。
- Data Section : 這裡放全域變數，大小固定。
- Text Section : 這裡放可執行的程式碼（指令），大小固定
OS 會確保 Stack Section 與 Heap Section 兩區域的資料不會覆蓋到彼此。   
:::warning
即使執行同個程式(Program)，使用 fork 產生新 process 時，process 的 Text Section、Data Section、Heap Section、Stack Section 也是不同的！
:::

### Program State Diagram 狀態圖，很重要
欸真的很重要看一下啦   
![image](https://hackmd.io/_uploads/BJ4y8QqkZe.png)   
- new : process 被創建
- running : CPU 時間內正在執行此程式
- waiting : process 正在等待某事件（如讀檔、等 IO）
- ready : 一個 process 在 ready queue 中等待 OS 將其分配到 processor 執行任務。
- terminated : process 人沒了。
### Process Control Block (PCB) 程序控制區塊
PCB 是 OS 中用來記錄並管理 Process 狀態、資源的資料結構，Scheduling 與 Context Switch 全都根據 PCB 內的資料將其掛載至各個 queue 中。   
![image](https://hackmd.io/_uploads/BkyMdQc1bg.png)   
- Process state : running / wating / ready / etc. 等狀態。
- Program counter (PC) : 下一個要執行的指令位址。
- CPU Reg. (registers) : 這裡的 Registers 是一種資料結構，而非真正的暫存器。當某個 Process 的 Time Slice 到期時，Process 中某些特定的暫存器之值就會被保存到 PCB 的 CPU Reg. 中，等到下一次執行時，這些暫存器的值會被讀取，並重新寫入 CPU　暫存器中。
- CPU Scheduling info. : 優先權、排程佇列的指標。
- Memory-management info. : 儲存分配給 Process 記憶體的管理資訊，內容可能包含 base / limit Reg. (基址 / 限位暫存器)、頁表 (Page Tables)、段表 (SegmentTables)。
- Accounting info. : CPU 使用時間、自 Process 啟動以來經過的時鐘時間、執行時間限制。
- IO state info. : 分配給 Process 的 IO 裝置、Process 開啟的檔案列表等等。

### Schedulers
分為 short-term 與 long-term ，現今大多數作業系統採用 short-term schedulers   

- Short-term : 
    - 在 ready queue 中選出下一個要拿到 CPU 的 Process。
    - 頻繁的被呼叫，且需要很快的切換(Time sharing)

- long-term : 
    - 決定哪些作 process 可以被載入到 ready queue 中。
    - 呼叫不頻繁，可以較慢

### Process Termination (Zombie / Orphan process) 肯定會考，看一下
- 殭屍程序（Zombie Process）：程序已經結束，但因為其父程序尚未讀取他的結束狀態，導致其仍然留於 Process Table 中。（已結束但父未讀其狀態而暫留在表中）
- 孤兒程序（Orphan Process）：某程序尚在執行時其父程序已經先終止了，這會讓子程序直接被init 程序 (PID = 1)收養。(父先終止，子由PID1收養)

### IPC in Shared memory / Message Passing System
- IPC in Shared memory System (記憶體共享)
    - ![image](https://hackmd.io/_uploads/HJ66V89k-g.png)
    - 多個 Process 之間共享一塊記憶體
    - 通訊過程由 user process 控制， OS 不直接介入。
    - Shared Memory 會引發同步問題。

- IPC in Message-Passing System (訊息傳遞)
    - ![image](https://hackmd.io/_uploads/HJf1H8c1be.png)
    - 讓 Process 之間彼此通訊並同步行為的機制，且不須共享相同的記憶體空間
    - 至少有兩種操作：`send / receive`
    - Communication Link 實作 :
        - 實體層：共享記憶體、硬體匯流排、網路
        - 邏輯層：直接 / 間接、同步 / 非同步、自動 / 顯式緩衝
    - 直接 / 間接通訊
        - 直接：Process 明確指明要給誰(哪個 Process)
        - 間接：
            - 訊息透過 mailbox (port) 收發。
            - 每個 mailbox (port) 有唯一的 uid。
            - 只有共享同一 mailbox (port) 的 Process 才能通訊。
    - 同步 / 非同步通訊 (阻塞與非阻塞通訊)
        - 同步 / 阻塞 (Synchronous / Blocking):
            - 同步傳送：送方直到訊息被收方接收才能繼續。
            - 同步接收：收方直到有新訊息才繼續。
            - 收方與送方：**有收到訊息才做下件事**
        - 非同步 / 非阻塞 (Asynchronous / non-blocking)
            - 非同步傳送：送方送出後立即繼續執行。
            - 非同步接收：收方要嘛拿到一個有效訊息(valid message)，要嘛拿到空訊息(null message)


## Chapter 4
### Thread 線程 ?
這個結構會考，要記   
![image](https://hackmd.io/_uploads/SJuAjU5J-x.png =70%x)
- **共享**所屬處理程序 (Process) 的資源
- 要叫 Process 老爸
- 創建成本低，Thread 之間切換成本也低 -> Light Weight
- 每個 Thread 都有一個自己的 PC、reg.、stack
- 與同個 Process 中的其他 Thread 共享 Text Section、Data Section 與其他 OS 的資源。
- 一個 Program 內可以有多個 Threads，Program 當中的不同任務也可以使用 Threads 來實現（比如畫面更新、回應網路請求）
- Kernal 本身通常也是 MultiThreads 的
- Threads 的好處：
    - 保證回應性：當 Process 的某部分被阻塞時，能確保其他 Threads 仍然能夠正常執行 (e.g. User Interface 作為 main Threads，保證其他 tasks 不影響使用者操作)
    - 資源共享：Threads 之間共享 Process 的某些資源，相較於 Process 的 shared mem. / Message-Passing 共容易相互溝通協調
    - 經濟性與可擴展性：相較建立新 Process 更省資源，Threads 之間的切換也比 Process 之間的切換負擔更輕，還能夠充分利用多處理器 (Multiprocessor) 架構
### Multicore Programming 多核心程式設計
- 在多核心 / 多處理器上，將工作拆分成可並行的子任務，再將子任務分配到多個核心同時執行，以提升效能。
- 將一個程式撰寫成多 Multicore 相容會面對許多挑戰，包含：
    - 任務拆分：理想上每個任務都是彼此獨立，能夠在不同核心並行執行
    - 負載均衡：每個核心承擔的任務量差不多
    - 資料切分：確定如何將資料有效地分割給不同的核心處理
    - 資料相依性：當兩個 Threads 同時存取一個變數時，會產生同步問題
- 平行化的類型( 會考 )：
    - 資料平行（Data Parallelism）：把同一資料集切片到多核心，各核心做相同運算。（類似作業一的找質數那樣）
    - 任務平行（Task Parallelism）：把不同任務分派到不同核心，各自做不同工作（有兩個不同的事情要處理，比如 Word，Thread A 做自動儲存、Thread B 做拼字檢查）

### Amdal's Law 阿爾達姆定律
理想上，MultiCore Programming 應該要能把工作切分並均分到所有核心，讓所有核心有相同的負載。但實務上，有些任務是不能夠被拆分的，這些任務是必須序列地被執行。
Amdal's Law 就是用來評估當一個程式同時包含序列與平行的部分時，在系統中加入核心能夠帶來多少效能提升？
$$speedUP \le \frac{1}{S+\frac{(1-S)}{N}}$$

其中
- $S$：序列任務所占的比例
- $N$：處理器核心數
- 當 $N→∞$ 時，加速比趨近於 $\frac{1}{S}$

若一個 Process 有 $75\%$ 是平行的任務， $25\%$ 是序列的任務，若核心數從1核升級成2核心，那麼
$$\frac{1}{0.25+\frac{(1-0.25)}{2}} = \frac{1}{0.25+0.375} = 1.6$$

加速比為 $1.6$。

### MultiThreading Models 多執行續模型
- Many-to-One 多對一
    ![image](https://hackmd.io/_uploads/BJUClO9k-x.png =70%x)
    - User 端有多個 Threads，OS 端只有一個 Thread
    - 其中一個 Thread 發生阻塞時，就會導致所有 Thread 阻塞。
    - **模擬出來的，不是真正的"同時"**

- One-to-One 一對一
    ![image](https://hackmd.io/_uploads/HkwZ-dcyZg.png =70%x)
    - 每個 user threads 都對應到一個 kernal threads。
    - 如果有大量的 Thread 時，會導致系統的效能負擔。
    - 現今 OS 多採用此設計。
    
- Mant-to-many 多對多
    ![image](https://hackmd.io/_uploads/HkXQWuckbe.png =70%x)
    - 把多個 user threads 對應到相同或較少數量的 kernal threads 執行。

- Two-level 混和模型
    ![image](https://hackmd.io/_uploads/rygKEW_ck-e.png =70%x)
    - 混合了 one-to-one Modal 與 many-to-many Modal，較為重要的 Thread 採用 one-to-one，剩下的用 many-to-many。

## Chapter 5
### CPU IO Burst Cycle
程式執行的過程中，CPU 執行的時間(CPU Burst) 與 IO 等待時間的週期性循環，就是 CPU IO Burst Cycle
### Preemptive / Non-Preemptive Scheduling 搶佔式與非搶佔式排程
- Preemptive 搶佔式排程
    - 當有更高優先權或更短剩餘時間的 Process 到來時，排成器可以隨時中斷目前正在執行的 Process 並切換成其他 Process 執行。
    - 現今作業系統多採用搶佔式排程。
- Non-Preemptive 非搶佔式排程
    - 一旦 Process 拿到 CPU，就會跑完成、阻塞或直到其自願讓出為止，中途 CPU 資源不會被奪走。
    - 不好管理，可能導致護航效應（Convoy Effect）。

### Scheduling Criteria 評量一個排程演算法的好壞
- **最大化** CPU 使用率 -> 讓 CPU 越忙越好
- **最大化** 吞吐量 -> 最大化相同時間內完成的 Process 數
- **最小化** 周轉時間(Turnaround Time) -> 一個 Process 從進 Ready Queue 到執行完的時間
- **最小化** 等待時間 -> 一個 Process 在 Ready Queue 中等待的時間
- **最小化** 響應時間 -> 從使用者 / 程式發出一個請求，到系統開始回應這個請求的所需時間

:::info
現今個人電腦比較強調**等待時間**與**響應時間**的最小化。
:::

### Scheduling Algorithm: FCFS / SJF 必考
- FCFS (First Come First Service)
    - 誰先來，誰就先用，忽略 Time Sharing
    
    | Process | Burst Time(運行時間) |
    | ------- | -------------------- |
    | P1      | 24                   |
    | P2      | 3                    |
    | P3      | 3                    |
    - 若 Process 的抵達順序為 P1、P2、P3，則甘特圖為：
        ![image](https://hackmd.io/_uploads/H17GGtq1Ze.png)
        - Wating time for each Process
            - P1 : 0
            - P2 : 24
            - P3 : 27
        - 平均 waiting time: $(0+24+27)/3 = 17$
    
    - 如果我們假設 Process 的抵達順序變為 P2、P3、P1，那們甘特圖會變成：
        ![image](https://hackmd.io/_uploads/BkphMY91-l.png)
        - Wating time for each Process
            - P1 : 6
            - P2 : 0
            - P3 : 3
        - 平均 waiting time: $(6+0+3)/3 = 3$
    - FCFS 會有很嚴重的護航效應(Convoy Effect) : 短的工作被拖在長的工作後面
        - 在不可搶佔式的 FCFS 中，若長的 CPU-bound 先拿到 CPU，就會跑得很久，其他IO bound Process 雖然每次只要一點點 CPU 就能回去做 IO，卻只能排隊等。
        - 導致短工作的平均 wating time 暴增，回應時間變差。

- Shortest Job First Scheduling
    - 使用 Process 的執行時間來排程，誰執行時間短誰就先做。
    - 雖然聽起來很直覺，但是個很 **理想的算法** :
        - 我們沒辦法預知未來，當然也就沒辦法得知每個 Process 需要執行多久
        - 只能透過先前使用情形來預測來預測
        - 常見的預測時間算法為指數平滑法 (Exponential smoothing formula)
    - Non-Preemptive SJF Example:
    
        | Process | Burst Time(運行時間) |
        | ------- | -------------------- |
        | P1      | 6                    |
        | P2      | 8                    |
        | P3      | 7                    |
        | P4      | 3                    |
    - 甘特圖：
    ![image](https://hackmd.io/_uploads/HyF9NYqJWe.png)
    - 平均 waiting time = $(0+3+9+16)/4 = 7$
    - SJF 有分成 Preemptive 與 Non-Preemptive 的版本
        - Preemptive 的 SJF 改比**剩下要執行的時間**，剩越短的優先。
        
        | Process | Arrival Time(抵達時間) | Burst Time (運行時間) |
        | ------- | ---------------------- | --------------------- |
        | P1      | 0                      | 8                     |
        | P2      | 1                      | 4                     |
        | P3      | 2                      | 9                     |
        | P4      | 3                      | 5                     |

        - 甘特圖與說明：
        
        ![image](https://hackmd.io/_uploads/H16qUYqybe.png)
        
        - Preemptive Ver. SJF Avg waiting Time:
            - $[(10-1)+(1-1)+(17-2)+(5-3)]/4 = 26/4 = 6.5$
            - P1 : $10-1$，中間等了 $9$ 個單位時間
            - P2 : $1-1$ ，一進來直接開始做
            - P3 : $17-2$，開始執行的時間減去抵達時間。
            - P4 : $5-3$，開始執行的時間減去抵達時間。
        - Non-Preemptive Ver. SJF Avg waiting Time:
            - 先抵達的先，中間不可以換人 && 執行時間短的先。
            - 執行順序：P1 -> P2 -> P3 -> P4
            - $[0+(8-1)+(12-2)+(17-2)]/4 = 31/4 = 7.75$

### Scheduling Algorithm: Round Robin
- RR 非常像 FCFS，但是是 Preemptive 的，避免了護航效應，
- 規定了一個時間切片(time slice) $q$，每個 task 經過單位時間 $q$ 後就一定要換人。
- RR 只是確保了每個 Process 執行的公平性，但其 Waiting Time 不見得會勝過 SJF。
- Example: $q=4$

    | Process | Burst Time(運行時間) |
    | ------- | -------------------- |
    | P1      | 24                   |
    | P2      | 3                    |
    | P3      | 3                    |
    
    - 甘特圖：
    ![image](https://hackmd.io/_uploads/r1FCFY9Jbg.png)
    - 平均 Waiting Time:
        - P1 : $10-4=6$，只有中間等了 $6$ 秒。
        - P2 : $4$
        - P3 : $7$
        - $(6+4+7)/3 = 5.6$
- 在 RR 中，決定 $q$ 是一件很重要的事情。
    - 如果 $q$ 太大，那麼 RR 就會直接變回 FCFS。
    - 如果 $q$ 太小，那麼會發生太多次 Context Switch，時間開銷大。
    - 一般來說會訂在 10 - 100 ms 之間。
    - 對於周轉時間 (Turnaround Time) 的影響：
        - 周轉時間是一個 Process 從進 Ready Queue 到執行完的時間。
        - 一般來說，如果多數 Process 能在單個時間片下完成他們**下一次**的 CPU Burst，就能夠改善平均周轉時間。
        - 理想上，大約 80% 的 CPU Burst 應該要短於 $q$

### Multi-Processor Scheduling
- Asymmetric multiprocessing : 由一顆特別強的 Process 來排成其他所有 Process。
- Symmetric multiprocessing (SMP) : 
    - 每個 Process 都自己排程自己。
    - 現今的主流做法

### Real-Time CPU Scheduling
- 對於每個任務，都會設定一個截止時間。
- 對於"即時"的定義，分成兩種：
    - 軟即時 (Soft real-time)：不保證任務一定會在截止時間內完成，盡量趕上。
    - 硬即時 (Hard real-time)：任務必須在截止時間內被服務完成，超時等同系統失敗。
- Priority-based Scheduling, PBS
    - 以優先權來決定誰先用 CPU
    - 排成器需支援 Preemptive, 以優先權為基礎的排程
        - 這樣只能保證軟即時
    - 確保 PBS 硬即時：
        - 將 Process 視為一個週期
        - 每個 Process 都有處理時間 $t$、截止時間 $d$、週期 $p$，$0 \le t \le d \le p$
        - 週期性任務的速率為 $1/p$
    ![image](https://hackmd.io/_uploads/SkQBZq9kWl.png)

- Rate-Monotonic Scheduling, RMS
    - 速率單調排程，固定優先權的演算法
    - 依照週期的倒數來指派優先權，週期越短，優先權越高。
    - 週期 = 死線（$p=d$）
    - Example (key: $p$ 小的先做):
        - P1 : $p_1 = 50, t_1 =20$
        - P2 : $p_2 = 100, t_2 =35$
        ![image](https://hackmd.io/_uploads/Hy4rXcck-e.png)

- Earliest-Deadline-First Scheduling, EDF
    - 死線先到先排，優先權越高
    - 不強制要求 Process 一定要是週期性的
    - EDF 沒有固定的 Piority。
    - 週期 = 死線（$p=d$）
    - Example:
        - P1 : $p_1 = 50, t_1 =25$
        - P2 : $p_2 = 80, t_2 =35$
        ![image](https://hackmd.io/_uploads/B1XvN951-l.png)
        - 相同情況下，如果用 RMS 的排法，會導致 $P_2$ 的第一個周期趕不上死線：
        ![image](https://hackmd.io/_uploads/S1WhN9qJ-l.png)
### Algorithm Evalution
- 在評估一個排程演算法的好壞之前，我們必須先決定 [Criteria](#Scheduling-Criteria-評量一個排程演算法的好壞)
- 四種評估方法：
    - 決定性建模 Deterministic Modeling：預先給定一個事先決定好的資料集來評估
    - 排隊理論 Queueing Models：利用數學表達式來描述 Process 的抵達，例如利特爾法則： $n = λW$，其中 $n$ 為平均隊列長度，$W$ 為隊列中的平均等待時間，$λ$進入隊列的平均到達率。
    - 模擬：利用模擬器來評估一個演算法的好壞。
    - 實作測試：直接在實機上進行測試，高風險與高成本。