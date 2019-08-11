<template>
  <div class="wrapper">
    <div class="sidebar" data-color="azure" data-background-color="white">
      <div class="logo">
        <router-link to="http://www.creative-tim.com" class="simple-text logo-normal">DocStore</router-link>
      </div>
      <div class="sidebar-wrapper">
        <ul class="nav">
          <li class="nav-item active">
            <router-link class="nav-link" to="/">
              <i class="material-icons"></i>
              <p>My Drive</p>
            </router-link>
          </li>
          <li class="nav-item">
            <router-link class="nav-link" to="/shared-with-me">
              <i class="material-icons"></i>
              <p>Shared With Me</p>
            </router-link>
          </li>
          <li class="nav-item">
            <router-link class="nav-link" to="/recent">
              <i class="material-icons"></i>
              <p>Recent</p>
            </router-link>
          </li>
          <li class="nav-item">
            <router-link class="nav-link" to="/stared">
              <i class="material-icons"></i>
              <p>Stared</p>
            </router-link>
          </li>
          <li class="nav-item">
            <router-link class="nav-link" to="/bin">
              <i class="material-icons"></i>
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
export default {
  
}
</script>


<style>
</style>
