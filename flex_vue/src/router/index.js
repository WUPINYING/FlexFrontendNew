import { createRouter, createWebHistory } from 'vue-router';
import Home from '../views/home/Home.vue';
import User from '../views/user/User.vue';
import Login from '../views/user/Login.vue';
import ActivityInfo from '../views/activity/ActivityInfo.vue';
import ActivitySignUp from '../views/activity/ActivitySignUp.vue';
const webTitle = 'FLEX - ';

// 路由設定
const routes = [
  {
    //http://loaclhost/
    path: '/',
    component: Home,
    meta: { title: `${webTitle}首頁` },
  },
  {
    //http://loaclhost/
    path: '/cart',
    component: () => import('../views/home/Cart.vue'),
    meta: { title: `${webTitle}購物車` },
  },
  {
    //http://loaclhost/User
    path: '/user',
    component: User,
    meta: { title: `${webTitle}會員` },
  },
  {
    //http://loaclhost/Login
    path: '/login',
    component: Login,
    meta: { title: `${webTitle}登入` },
  },
  {
    //http://loaclhost/orders
    path: '/orders',
    component: () => import('../views/orders/orderindex.vue'),
    meta: { title: `${webTitle}訂單` },
  },
  {
    //http://loaclhost/activityInfo
    path: '/activityInfo',
    component: ActivityInfo,
    meta: { title: `${webTitle}活動` },
  },
  {
    //http://loaclhost/activitySignUp
    path: '/activitySignUp',
    component: ActivitySignUp,
    meta: { title: `${webTitle}活動報名` },
  },
  {
    //http://loaclhost/Men
    path: '/men',
    component: () => import('../views/product/ProductMenLayout.vue'),
    children: [
      {
        path: '',
        component: () => import('../views/product/ProductMen.vue'),
      },
      {
        path: ':categoryName',
        component: () => import('../views/product/ProductMen.vue'),
      },
      {
        path: ':categoryName/:subCategoryName',
        component: () => import('../views/product/ProductMen.vue'),
      },
      {
        // 當 /ProductMenLayout/:id/posts 匹配成功
        // Detial.vue 將被渲染到 ProductMenLayout 的 <router-view> 内部，替換card.vue
        path: 'detail/:prdouctId',
        component: () => import('../views/product/ProductDetail.vue'),
      },
    ],
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes: routes,
  //  變數名稱跟屬性名稱一樣可直接省略成 routes
});

router.beforeEach((to, from, next) => {
  if (to.meta.title) {
    document.title = to.meta.title;
  }
  next();
});

export default router;
