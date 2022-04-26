<template>
  <v-navigation-drawer
    id="default-drawer"
    v-model="$store.state.home.drawer"
    :dark="dark"
    :right="$vuetify.rtl"
    :src="drawerImage ? image : ''"
    :mini-variant.sync="$store.state.home.mini"
    mini-variant-width="80"
    app
    width="260"
  >
    <template v-if="drawerImage" #img="props">
      <v-img :key="image" :gradient="gradient" v-bind="props" />
    </template>

    <div class="px-2">
      <default-drawer-header />

      <v-divider class="mx-3 mb-2" />

      <default-list :items="items" />
    </div>

    <template #append>
      <div class="pa-4 text-center">
        <app-btn
          class="text-none mb-4"
          color="white"
          href="https://vuetifyjs.com"
          small
          text
        >
          Documentation
        </app-btn>

        <app-btn block class="text-none" color="secondary" @click="logout">
          <v-icon left> mdi-package-up </v-icon>

          退出
        </app-btn>
      </div>
    </template>

    <div class="pt-12" />
  </v-navigation-drawer>
</template>

<script>
// Utilities
// import { get, sync } from 'vuex-pathify'
import userApi from "@/api/userApi";
export default {
  methods: {
    init() {
      //这里动态获取菜单，暂时写死
      // userApi.GetMenuByHttpUser().then((resp) => {
      //   this.items = resp.data.children;
      // });
      this.items = 
      
       [
          {
            icon: "mdi-view-dashboard",
            router: "/",
            menu_name: "首页",
            roles: null,
            mould: null,
            is_top: 0,
            sort: 0,
            is_show: 1,
            parentId: 1,
            children: null,
            id: 2,
            is_delete: 0,
          },
          {
            icon: "mdi-account-box-multiple",
            router: null,
            menu_name: "用户角色管理",
            roles: null,
            mould: null,
            is_top: 0,
            sort: 0,
            is_show: 1,
            parentId: 1,
            children: [
              {
                icon: "mdi-account-box",
                router: "/AdmUser/",
                menu_name: "用户管理",
                roles: null,
                mould: null,
                is_top: 0,
                sort: 0,
                is_show: 1,
                parentId: 3,
                children: null,
                id: 4,
                is_delete: 0,
              },
              {
                icon: "mdi-account-circle",
                router: "/admrole/",
                menu_name: "角色管理",
                roles: null,
                mould: null,
                is_top: 0,
                sort: 0,
                is_show: 1,
                parentId: 3,
                children: null,
                id: 9,
                is_delete: 0,
              },
            ],
            id: 3,
            is_delete: 0,
          },
          {
            icon: "mdi-account-cash",
            router: null,
            menu_name: "角色接口管理",
            roles: null,
            mould: null,
            is_top: 0,
            sort: 0,
            is_show: 1,
            parentId: 1,
            children: [
              {
                icon: "mdi-clipboard-check-multiple",
                router: "/AdmMenu/",
                menu_name: "菜单管理",
                roles: null,
                mould: null,
                is_top: 0,
                sort: 0,
                is_show: 1,
                parentId: 14,
                children: null,
                id: 15,
                is_delete: 0,
              },
              {
                icon: "mdi-circle-slice-8",
                router: "/admMould/",
                menu_name: "接口管理",
                roles: null,
                mould: null,
                is_top: 0,
                sort: 0,
                is_show: 1,
                parentId: 14,
                children: null,
                id: 20,
                is_delete: 0,
              },
              {
                icon: "mdi-clipboard-account",
                router: "/admRoleMenu/",
                menu_name: "角色菜单分配管理",
                roles: null,
                mould: null,
                is_top: 0,
                sort: 0,
                is_show: 1,
                parentId: 14,
                children: null,
                id: 25,
                is_delete: 0,
              },
            ],
            id: 14,
            is_delete: 0,
          },
          {
            icon: "mdi-clipboard-flow-outline",
            router: null,
            menu_name: "路由管理",
            roles: null,
            mould: null,
            is_top: 0,
            sort: 0,
            is_show: 1,
            parentId: 1,
            children: [
              {
                icon: "mdi-account-eye",
                router: "/userinfo/",
                menu_name: "用户信息",
                roles: null,
                mould: null,
                is_top: 0,
                sort: 0,
                is_show: 1,
                parentId: 26,
                children: null,
                id: 27,
                is_delete: 0,
              },
              {
                icon: "mdi-account-eye",
                router: "/pan",
                menu_name: "云盘管理",
                roles: null,
                mould: null,
                is_top: 0,
                sort: 0,
                is_show: 1,
                parentId: 26,
                children: null,
                id: 28,
                is_delete: 0,
              },
            ],
            id: 26,
            is_delete: 0,
          },
        ]

    },
    logout() {
      this.$store.dispatch("Logout");
      this.$router.push({ path: "/login" });
    },
  },
  created() {
    this.init();
  },
  computed: {
    image() {
      return this.$store.getters.image;
    },
    gradient() {
      return this.$store.getters.gradient;
    },
    drawerImage() {
      return this.$store.state.home.drawerImage;
    },
    dark() {
      return this.$store.state.user.dark;
    },
  },

  data: () => ({
    items: [],
  }),
  name: "DefaultDrawer",

  components: {
    DefaultDrawerHeader: () =>
      import(
        /* webpackChunkName: "default-drawer-header" */
        "./widgets/DrawerHeader"
      ),
    DefaultList: () =>
      import(
        /* webpackChunkName: "default-list" */
        "./List"
      ),
  },
  // computed: {
  //   ...get('user', [
  //     'dark',
  //     'gradient',
  //     'image',
  //   ]),
  //   ...get('app', [
  //     'items',
  //     'version',
  //   ]),
  //   ...sync('app', [
  //     'drawer',
  //     'drawerImage',
  //     'mini',
  //   ]),
  // },
};
</script>

<style lang="sass">
#default-drawer
  .v-list-item
    margin-bottom: 8px

  .v-list-item::before,
  .v-list-item::after
    display: none

  .v-list-group__header__prepend-icon,
  .v-list-item__icon
    margin-top: 12px
    margin-bottom: 12px
    margin-left: 4px

  &.v-navigation-drawer--mini-variant
    .v-list-item
      justify-content: flex-start !important
</style>
