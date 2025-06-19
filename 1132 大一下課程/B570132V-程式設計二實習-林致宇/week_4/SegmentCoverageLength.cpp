    #include <bits/stdc++.h>
    #define whitebear ios_base::sync_with_stdio(0), cin.tie(0) , cout.tie(0)
    #define vi vector<int>
    #define vii vector<vector<int>>
    #define pii pair<int,int>
    #define f first
    #define s second
    #define int long long
    using namespace std;
     
    //usage cin >> n -> n = nextint()
     
    inline char readchar() {
        const int S = 1<<20; // buffer size
        static char buf[S], *p = buf, *q = buf;
        if(p == q && (q = (p=buf)+fread(buf,1,S,stdin)) == buf) return EOF;
        return *p++;
    }
     
    inline int nextint() {
        int x = 0, c = getchar(), neg = false;
        while(('0' > c || c > '9') && c!='-' && c!=EOF) c = getchar();
        if(c == '-') neg = true, c = getchar();
        while('0' <= c && c <= '9') x = x*10 + (c^'0'), c = getchar();
        if(neg) x = -x;
        return x; // returns 0 if EOF
    }
    
    inline signed solve(){
        int m = 0;
        m = nextint();
        vector<pii> uwu;
        for ( int i = 0 ; i < m ; i++ ){
            int l = 0, r = 0;
            l = nextint() , r = nextint();
            uwu.push_back({l,r});
        }
        sort(uwu.begin(), uwu.end());
        int ans = 0;
        int st = -1, end = -1;
        for ( auto now : uwu ){
            if ( now.f > end ){ //如果區間的開始大於當前終點 update it!
                if ( st != -1 ) ans += end - st;
                st = now.f , end = now.s;
            }else{ // segment merge
                end = max(end,now.s);
            }
        }
        if ( st != -1  ) ans += end - st;
        cout << ans << "\n";
        return 0;
    }
    
    signed main(){
        whitebear;
        int t = 0;
        t = nextint();
        while(t--)
            solve();
        return 0;
    }