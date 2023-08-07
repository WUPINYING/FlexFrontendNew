<template>
  <div class="container loginBox">
    <div>
      <h4>LOGIN</h4>
    </div>
    <div class="from-group" v-if="errors.length">
      <ul>
        <span v-for="error in errors" class="text-danger">{{ error }}</span>
      </ul>
    </div>
    <div class="from-group mb-3">
      <input
        type="text"
        name="account"
        v-if="accInput"
        v-model="account"
        class="form-control"
        placeholder="手機號碼/帳號/Email"
      />
    </div>
    <div class="from-group mb-3">
      <label>密碼</label>
      <input
        type="password"
        name="password"
        v-if="validated"
        class="form-control"
        placeholder="密碼"
      />
    </div>
    <div class="from-group mb-3">
      <label>信箱</label>
      <input
        type="email"
        name="email"
        v-if="unValidated"
        class="form-control"
        placeholder="信箱"
      />
    </div>
    <div class="from-group mb-3">
      <button
        type="submit"
        class="btn btn-danger logBtn"
        @click="ValidatedIdentity"
      >
        登入 / 註冊
      </button>
    </div>
    <div>
      <p>或</p>
    </div>
    <div class="from-group mb-3">
      <button
        type="submit"
        class="btn btn-info googleBtn"
        @click="ValidatedIdentity"
      >
        我是假的google登入
      </button>
    </div>
  </div>
</template>

<script setup>
import axios from 'axios';
import { ref } from 'vue';

const errors = ref([]);
const userData = ref([]);
const accInput = ref(true);
const validated = ref(false); //初始化狀態為不顯示
const unValidated = ref(false);

const account = ref('');
//const password = ref('');

const baseAddress = 'https://localhost:7183';
const uri = `${baseAddress}/api/Users/Login`;

function ValidatedIdentity() {
  //alert('loginAndRegister');
  //未填寫
  if (account.value === '') {
    errors.value = [];
    errors.value.push('沒有填誰知道你是誰');
  } else {
    //已填寫
    var loginData = {}; //儲存傳給後端的登入資料
    loginData.Account = account.value;
    //console.log(loginData.Account.value);

    axios
      .post(uri, loginData)
      .then((res) => {
        userData.value = res.data;
        //console.log(userData.value); //後端return的訊息

        //從回傳的資料中取得帳號並進行比較
        if (userData.value === account.value) {
          console.log('帳號驗證成功囉!');
          validated.value = true;
          accInput.value = false;
        } else {
          validated.value = false;
          console.log('帳號驗證失敗');
          unValidated.value = true;
        }
      })
      .catch((err) => {
        alert('API請求失敗：' + err.message);
      });
  }
}
</script>
<style>
.loginBox {
  border: solid;
}

.logBtn {
  width: 150px;
}
p {
  position: relative;
  text-align: center; /* 將文字置於中間 */
}

p::before,
p::after {
  content: '';
  position: absolute;
  top: 50%;
  width: 220px; /* 調整線段的長度 */
  height: 1px; /* 調整線段的粗細 */
  background-color: black; /* 設定線段的顏色 */
}

p::before {
  left: 0;
}

p::after {
  right: 0;
}
</style>
