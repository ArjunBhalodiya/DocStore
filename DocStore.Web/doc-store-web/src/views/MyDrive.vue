<template>
  <div class="main-panel">
    <!-- Navbar -->
    <nav class="navbar navbar-expand-lg navbar-transparent navbar-absolute fixed-top">
      <div class="container-fluid">
        <div class="navbar-wrapper">
          <i class="material-icons fa fa-hdd-o fa-2x" aria-hidden="true"></i>
          <a class="navbar-brand" href="#pablo">My Drive</a>
        </div>
        <button
          class="navbar-toggler"
          type="button"
          data-toggle="collapse"
          aria-controls="navigation-index"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span class="sr-only">Toggle navigation</span>
          <span class="navbar-toggler-icon icon-bar"></span>
          <span class="navbar-toggler-icon icon-bar"></span>
          <span class="navbar-toggler-icon icon-bar"></span>
        </button>
        <div class="collapse navbar-collapse justify-content-end">
          <ul class="navbar-nav">
            <li class="nav-item dropdown">
              <a
                class="nav-link"
                href="#pablo"
                id="navbarDropdownProfile"
                data-toggle="dropdown"
                aria-haspopup="true"
                aria-expanded="false"
              >
                <i class="material-icons">person</i>
                <p class="d-lg-none d-md-block">Account</p>
              </a>
              <div
                class="dropdown-menu dropdown-menu-right"
                aria-labelledby="navbarDropdownProfile"
              >
                <a class="dropdown-item" href="#">Profile</a>
                <a class="dropdown-item" href="#">Settings</a>
                <div class="dropdown-divider"></div>
                <a class="dropdown-item" href="#">Log out</a>
              </div>
            </li>
          </ul>
        </div>
      </div>
    </nav>
    <div class="content">
      <div class="container-fluid">
        <div class="row">
          <div class="col-lg-3 col-md-6 col-sm-6" v-for="Document in Documents" v-bind:key="Document.id">
            <Card :document="Document" />
          </div>
          <!-- <div class="col-lg-3 col-md-6 col-sm-6">
            <Card :document="Documents[1]" />
          </div>
          <div class="col-lg-3 col-md-6 col-sm-6">
            <Card :document="Documents[2]" />
          </div> -->
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Documents from "@/data/documents.js";
import HelloWorld from "@/components/HelloWorld.vue";
import Card from "@/components/Card.vue";

export default {
  name: "mydrive",
  data() {
    return { Documents };
  },
  components: {
    HelloWorld,
    Card
  }
};
</script>
