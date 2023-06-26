<template>
  <div class="container-fluid content-container">
    <div class="row">
      <div class="col-7">
        <DxGrid :data-url="urlGetUsers" :columns="userColumns" />
      </div>
      <div class="col-5">
        <DxGrid :data-url="urlGetRoles" :columns="roleColumns" />
      </div>
    </div>
  </div>
</template>

<script>
import "devextreme/dist/css/dx.light.css";

import DxGrid from "./DxGrid.vue";
import {DataGrid} from "./DxGrid.vue";

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
          caption: "Филиал",
          dataField: "FilialName",
          filterOperations: ["=", "<>"],
          lookup: DataGrid.getColumnLookup("/Filials/GetFilialsForSearchField"),
        },
        {
          caption: "Подразделение",
          dataField: "DepartmentName",
        },
        {
          caption: "Должность",
          dataField: "PostName",
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
        {
          caption: "Группа AD",
          dataField: "GroupAD",
        },
        {
          caption: "Изменил",
          dataField: "ChangedBy",
        },
        {
          caption: "Дата последнего изменения",
          dataField: "DateLastEdit",

          dataType: "date",
          format: DataGrid.getShortDateTimeFormat(),
          calculateFilterExpression: DataGrid.getCalculateFilterExpression,
        },
        {
          caption: "По умолчанию",
          dataField: "IsDefault",

          dataType: "boolean",
        },
      ],
    };
  },
};
</script>
<style></style>
