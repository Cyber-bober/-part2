#include <iostream>
using namespace std;
int main(){
    cin.tie(0);
    ios::sync_with_stdio(false);
    int t;
    cin >> t; 
    while (t--){
        int n;
        int a, cnt0 = 0, cnt1 = 0;
        int b, m1;
        cin >> n;
        for(int i = 0; i < 2*n; i++){
            cin >> a;
            if (a == 0) {
                cnt0 += 1;
            }
            else {
                cnt1 += 1;
            }
        }
        int m0;
        if (cnt0 % 2 == 0){
            m0 = 0;
        }
        else {
            m0 = 1;
        }
        if (cnt1 > cnt0){
            m1 = cnt0;
        }
        else {
            m1 = cnt1;
        }
        cout << m0 << ' ' << m1 << '\n';
    }
}
