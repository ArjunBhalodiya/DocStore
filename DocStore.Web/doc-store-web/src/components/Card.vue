<template>
  <div v-if="this.document.type === 'file'">
    <div class="card card-stats">
      <div class="card-header card-header-info card-header-icon">
        <div class="card-icon">
          <i class="material-icons fa fa-file"></i>
        </div>
        <h4 class="card-title">
          {{this.document.name}}
          <h6>{{this.document.extention}}</h6>
        </h4>
      </div>
      <div class="card-footer">
        <div class="stats">
          <i class="material-icons fa fa-archive"></i>
          &nbsp;{{this.document.size}}
        </div>
      </div>
    </div>
  </div>
  <div v-else>
    <div class="card card-stats">
      <div class="card-header card-header-success card-header-icon">
        <div class="card-icon">
          <i class="material-icons fa fa-folder"></i>
        </div>
        <h4 class="card-title">{{this.document.name}}</h4>
      </div>
      <div class="card-footer">
        <div class="stats">
          <i class="material-icons fa fa-archive"></i>
          &nbsp;{{this.document.noOfFiles}} files
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Card",
  props: { document: Object },
  data() {
    return { document };
  }
};
</script>

<style scoped>
.card-header {
  min-height: 90px;
}
</style>