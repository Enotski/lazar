<template>
  <div class="container-fluid content-container">
    <div class="d-flex flex-row">
      <div>
        <DxGrid
          :data-url="urlGetUsers"
          :columns="userColumns"
          :events="userEvents"
          :height="usersTableHeight"
        />
      </div>
      <div class="row ml-5 pt-0 pl-0">
        <div class="row mb-3">
          <div class="col-auto p-0">
            <v-btn
              icon="mdi-plus-circle-outline"
              variant="text"
              title="Set Role"
              class="text-secondary"
              size="small"
              rounded="lg"
              @click="setRoleToUser"
            ></v-btn>
          </div>
          <div class="col pr-0">
            <DxSelect
              :ref="rolesSelectRef"
              :data-url="urlGetRolesList"
              :params-data="paramsData"
              :height="38"
            />
          </div>
        </div>
        <div class="row">
          <DxGrid
            :ref="rolesGridRef"
            :data-url="urlGetRoles"
            :columns="roleColumns"
            :height="rolesTableHeight"
            :params-data="paramsData"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import DxGrid from "./DxGrid.vue";
import DxSelect from "./DxSelect.vue";

export default {
  components: {
    DxGrid,
    DxSelect,
  },
  computed: {
    dxRolesGrid: function () {
      return this.$refs[this.rolesGridRef].getDxGrid();
    },
    dxUsersGrid: function () {
      return this.$refs[this.usersGridRef].getDxGrid();
    },
    dxSelect: function () {
      return this.$refs[this.rolesSelectRef].getDxSelect();
    },
  },
  data() {
    return {
      urlGetUsers: "https://localhost:7188/Users/GetUsersDataGrid",
      urlGetRoles: "https://localhost:7188/Roles/GetRolesDataGrid",
      urlGetRolesList: "https://localhost:7188/Roles/GetRoles",
      urlSetRoleToUser: "https://localhost:7188/UserProfile/SetRoleToUser",
      usersGridRef: "users_grid",
      rolesGridRef: "roles_grid",
      rolesSelectRef: "roles_select",
      paramsData:{
        selectedUserId: ''
      },
      usersTableHeight: 600,
      rolesTableHeight: 544,
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
      userEvents: {
        onSelectionChanged: function (e) {
          this.paramsData.selectedUserId =
            e.selectedRowKeys !== undefined &&
            e.selectedRowKeys !== null &&
            e.selectedRowKeys.length > 0
              ? e.selectedRowKeys[0]
              : "";

          this.dxRolesGrid.refresh();
          this.dxSelect.getDataSource().load();
        }.bind(this),
      },
    };
  },
  methods: {
    setRoleToUser: async function () {
      let role = this.dxSelect.option("value");
      if (role !== undefined && role !== null) {
        let args = {
          id: this.paramsData.selectedUserId,
          roleId: role.Id,
        };
        let el = this;
        await fetch(this.urlSetRoleToUser, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(args),
        })
          .then(async function () {
            el.dxUsersGrid.refresh()
            el.dxRolesGrid.refresh();
          })
          .catch(() => {
            throw new Error("Data Loading Error");
          });
      }
    },
  },
};
</script>
<style></style>
