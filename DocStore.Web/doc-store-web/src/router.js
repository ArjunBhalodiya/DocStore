/* eslint-disable indent */
import Vue from 'vue';
import Router from 'vue-router';

Vue.use(Router);

export default new Router({
  routes: [{
      path: '/',
      name: 'my-drive',
      component: () => import('./views/MyDrive.vue'),
    },
    {
      path: '/shared-with-me',
      name: 'shared-with-me',
      component: () => import('./views/SharedWithMe.vue'),
    },
    {
      path: '/recent',
      name: 'recent',
      component: () => import('./views/Recent.vue'),
    },
    {
      path: '/stared',
      name: 'stared',
      component: () => import('./views/Stared.vue'),
    },
    {
      path: '/bin',
      name: 'bin',
      component: () => import('./views/Bin.vue'),
    },
  ],
});
