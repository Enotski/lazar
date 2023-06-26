<template>
  <div class="container-fluid content-container">
    <div class="d-flex flex-row">
      <div>
        <DxGrid :data-url="urlGetUsers" :columns="userColumns" :events="userEvents"/>
      </div>
      <div class="row">
        <div>
        </div>
        <div>
          <DxGrid :data-url="urlGetRoles" :columns="roleColumns" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import DxGrid from "./DxGrid.vue";

export default {
  components: {
    DxGrid,
  },
  data() {
    return {
      urlGetUsers: "https://localhost:7188/Users/GetUsersDataGrid",
      urlGetRoles: "https://localhost:7188/Roles/GetRolesDataGrid",
      userColumns: [
        {
          caption: "№ п/п",
          dataField: "Num",
          allowSorting: false,
          allowFiltering: false,
          width: 60,
        },
        {
          caption: "ФИО",
          dataField: "FullName",
        },
        {
          caption: "Логин",
          dataField: "Login",
        },
        {
          caption: "Эл.почта",
          dataField: "Email",
        },
        {
          caption: "Роли",
          dataField: "Roles",
        },
      ],
      roleColumns: [
        {
          caption: "№ п/п",
          dataField: "Num",
          allowSorting: false,
          allowFiltering: false,
          width: 60,
        },
        {
          caption: "Наименование",
          dataField: "Name",
        },
      ],
      userEvents:{
        onSelectionChanged: function(e) {
            
        }
      }
    };
  },
};
</script>
<style></style>
