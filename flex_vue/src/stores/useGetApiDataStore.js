import { defineStore } from 'pinia';
import { ref } from 'vue';
import axios from 'axios';
import Cookies from 'js-cookie';

//第一個參數getApiData指的是整個store的id
//官方建議pinia function 為use開頭
export const useGetApiDataStore = defineStore('getApiData', {
  state: () => {
    return {
      loginSuccess: ref(false),
      memberData: ref({}),
    };
  },
  actions: {
    setLoginSuccess(value) {
      this.loginSuccess = value;
      //這邊還沒有存成功我的大頭照
      //console.log(this.loginSuccess);
    },
    handleLogout() {
      this.memberData.value = {};
      this.memberId = null;
      Cookies.remove('user_cookie');

      const baseAddress = 'https://localhost:7183';
      const uri = `${baseAddress}/api/Users/Logout`;
      axios
        .delete(uri)
        .then((res) => {
          console.log('delet API');
        })
        .catch((err) => {
          console.error(err);
        });
    },
    setMemberUsername(username, memberId) {
      //組長還要一個memberId
      this.memberData.username = username;
      this.memberId = memberId;
    },
  },
});
