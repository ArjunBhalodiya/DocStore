<template>
  <div class="wrapper">
    <div class="sidebar" data-color="azure" data-background-color="white">
      <div class="logo">
        <router-link to="http://www.creative-tim.com" class="simple-text text-info">
          <h2>
            <i class="fa fa-cubes fa-1x" aria-hidden="true"></i>DocStore
          </h2>
        </router-link>
      </div>
      <div class="sidebar-wrapper">
        <ul class="nav">
          <li class="nav-item">
            <router-link class="nav-link" to="/" exact>
              <i class="material-icons fa fa-hdd-o" aria-hidden="true"></i>
              <p>My Drive</p>
            </router-link>
          </li>
          <li class="nav-item">
            <router-link class="nav-link" to="/shared-with-me" exact>
              <i class="material-icons fa fa-share-alt" aria-hidden="true"></i>
              <p>Shared With Me</p>
            </router-link>
          </li>
          <li class="nav-item">
            <router-link class="nav-link" to="/recent" exact>
              <i class="material-icons fa fa-clock-o" aria-hidden="true"></i>
              <p>Recent</p>
            </router-link>
          </li>
          <li class="nav-item">
            <router-link class="nav-link" to="/stared" exact>
              <i class="material-icons fa fa-star-o" aria-hidden="true"></i>
              <p>Stared</p>
            </router-link>
          </li>
          <li class="nav-item">
            <router-link class="nav-link" to="/bin" exact>
              <i class="material-icons fa fa-trash-o" aria-hidden="true"></i>
              <p>Bin</p>
            </router-link>
          </li>
        </ul>
      </div>
    </div>

    <router-view />
  </div>
</template>

<script>
export default {};
</script>


<style scoped>
.router-link-exact-active,
.router-link-exact-active:hover{
  background-color: #9c27b0 !important;
  box-shadow: 0 4px 20px 0px rgba(0, 0, 0, 0.14),
    0 7px 10px -5px rgba(156, 39, 176, 0.4) !important;
  color: #fff !important;
}
.router-link-exact-active > i {
  background-color: transparent;
  color: #fff !important;
}
</style>
