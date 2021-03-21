/*
T M Hridoy KUET CSE 15
*/

#include<bits/stdc++.h>
#include <ext/pb_ds/assoc_container.hpp>
using namespace __gnu_pbds;
using namespace std;
#define print cout<<"==================\n";
typedef    long long ll;
typedef pair<ll,ll   > payar;
typedef   unsigned long long ull;
/* Special functions:

        find_by_order(k) --> returns iterator to the kth largest element counting from 0
        order_of_key(val) --> returns the number of items in a set that are strictly smaller than our item
*/
///set
typedef tree<int,null_type,less<int>,rb_tree_tag,
        tree_order_statistics_node_update> indexed_set;
///multiset
typedef tree<payar,null_type,less<payar>,rb_tree_tag,
        tree_order_statistics_node_update>ordered_set;
///map
template<class key, class value, class cmp = std::less<key>>
using ordered_map = tree<key, value, cmp, rb_tree_tag, tree_order_statistics_node_update>;
ordered_map<int, int> my_map;
#define IOS ios::sync_with_stdio(0); cin.tie(0); cout.tie(0);
#define readf                freopen("in.txt", "r", stdin);
#define writef               freopen("out.txt", "w", stdout);
priority_queue<int, vector<int>,  greater<int> > aa,bb,both; ///accending
vector< pair<int,payar  > > vpp;
vector < payar >  vp,vp1;
#define pi acos(-1.0);
#define EPS 1e-9
#define  mod   1000000007
#define  modulo  1000000007
#define all(x)  x.begin(),x.end()
#define un(x)       x.erase(unique(all(x)), x.end())
#define endl "\n"
using namespace std;
const int M = 998244353;
#define mx 300005
const int N = 3e5+7;


int main()
{
    IOS;

    int t ;
    cin>>t;

    for(int tc = 1 ; tc<=t ; ++tc)
    {
        int n, m ;
        cin>>n>>m;
        int cumR[n+1][m+1];
        int cumU[n+1][m+1];
        int cumD[n+1][m+1];
        int cumL[n+1][m+1];
        int mat[n+1][m+1];
        memset(cumR,0, sizeof cumR);
        memset(cumU,0, sizeof cumU);
        memset(cumD,0, sizeof cumD);
        memset(cumL,0, sizeof cumL);
        for(int i = 1 ; i<=n ; ++i)
        {
            for(int j = 1 ; j<=m ; ++j)
            {
                cin>>mat[i][j];
            }
        }
        for(int i = 1 ; i<=n ; ++i)
        {
            for(int j = 1 ; j<=m ; ++j)
            {
                cumR[i][j]+=cumR[i][j-1]+mat[i][j];
            }
        }
        for(int i = 1 ; i<=n ; ++i)
        {
            for(int j = m ; j>=1 ; --j)
            {
                cumL[i][j]+=cumL[i][j+1]+mat[i][j];
            }
        }
        for(int j = 1 ; j<=m ; ++j)
        {
            for(int i = 1 ; i<=n ; ++i)
            {

                cumD[i][j]+=cumD[i][j-1]+mat[i][j];
            }
        }

        for(int j = m ; j>=1 ; --j)
        {
            for(int i = 1 ; i<=n ; ++i)
            {
                cumU[i][j]+=cumU[i][j+1]+mat[i][j];
            }
        }
        for(int i = 1 ; i<=n ; ++i)
        {
            for(int j = 1 ; j<=m ; ++j)
            {
                cout<<cumU[i][j]<<" ";
            }
            cout<<endl;
        }
        // cout<<"Case #"<<tc<<": "<<abs(dif-k)<<endl;
    }

    return 0;
}
