<template>
  <div class="container-fluid content-container">
    <div class="d-flex flex-row">
      <div>
        <DxGrid
          :ref="usersGridRef"
          :data-url="urlGetUsers"
          :columns="userColumns"
          :events="userEvents"
          :height="usersTableHeight"
          :editing="usersGridEditing"
          :data-edit-functions="usersEditFunctions"
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
              :disabled="selectDisabled"
              @click="setRoleToUser"
            ></v-btn>
          </div>
          <div class="col pr-0">
            <DxSelect
              :ref="rolesSelectRef"
              :data-url="urlGetRolesList"
              :params-data="paramsData"
              :height="38"
              :disabled="selectDisabled"
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
            :editing="rolesGridEditing"
            :data-edit-functions="rolesEditFunctions"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import DxGrid from "./DxGrid.vue";
import DxSelect from "./DxSelect.vue";

import sendRequest from "../../utils/requestUtils";

const URL = "https://localhost:7188";
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
    dxRolesSelect: function () {
      return this.$refs[this.rolesSelectRef].getDxSelect();
    },
  },
  data() {
    return {
      urlGetUsers: `${URL}/Users/GetUsersDataGrid`,
      urlGetRoles: `${URL}/Roles/GetRolesDataGrid`,
      urlGetRolesList: `${URL}/Roles/GetRoles`,
      urlSetRoleToUser: `${URL}/UserProfile/SetRoleToUser`,
      usersGridRef: "users_grid",
      rolesGridRef: "roles_grid",
      rolesSelectRef: "roles_select",
      selectDisabled: true,
      paramsData: {
        selectedUserId: "",
      },
      usersTableHeight: 600,
      rolesTableHeight: 544,
      rolesGridEditing: {
        allowAdding: true,
        allowUpdating: true,
        allowDeleting: true,
        confirmDelete: true,
        useIcons: true,
        mode: "row",
        refreshMode: "full",
      },
      usersGridEditing: {
        allowAdding: true,
        allowUpdating: true,
        allowDeleting: true,
        confirmDelete: true,
        useIcons: true,
        mode: "row",
        refreshMode: "full",
      },
      usersEditFunctions: {
        insert: async (values) =>
          await sendRequest(`${URL}/Users/AddUser`, "POST", values),
        update: async (key, values) =>
          await sendRequest(`${URL}/Users/UpdateUser`, "POST", {
            id: key,
            email: values.Email,
            login: values.Login,
          }),
        remove: async (key) =>
          await sendRequest(`${URL}/Users/DeleteUser`, "POST", {
            id: key,
          }),
      },
      rolesEditFunctions: {
        insert: async (values) =>
          await sendRequest(`${URL}/Roles/AddRole`, "POST", values).then(() => {
            this.updateRolesSelect();
          }),
        update: async (key, values) =>
          await sendRequest(`${URL}/Roles/UpdateRole`, "POST", {
            id: key,
            name: values.Name,
          }).then(() => {
            this.dxUsersGrid.refresh();
            this.updateRolesSelect();
          }),
        remove: async (key) => {
          if (this.paramsData.selectedUserId === "")
            await sendRequest(`${URL}/Roles/DeleteRole`, "POST", {
              id: key,
            }).then(() => {
              this.dxUsersGrid.refresh();
              this.updateRolesSelect();
            });
          else
            await sendRequest(`${URL}/Users/RemoveRoleFromUser`, "POST", {
              id: this.paramsData.selectedUserId,
              roleId: key,
            }).then(() => {
              this.dxUsersGrid.refresh();
              this.updateRolesSelect();
            });
        },
      },
      userColumns: [
        {
          caption: "№ п/п",
          dataField: "Num",
          allowSorting: false,
          allowFiltering: false,
          width: 60,
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
          this.dxRolesSelect.getDataSource().load();
          if (this.paramsData.selectedUserId === "") {
            this.selectDisabled = true;
            this.dxRolesSelect.reset();
          } else {
            this.selectDisabled = false;
          }
        }.bind(this),
      },
    };
  },
  methods: {
    updateRolesSelect() {
      let select = this.dxRolesSelect;
      select.getDataSource().load();
      select.reset();
    },
    setRoleToUser: async function () {
      let el = this;
      let role = this.dxRolesSelect.option("value");

      if (role !== undefined && role !== null) {
        let args = {
          id: this.paramsData.selectedUserId,
          roleId: role.Id,
        };
        await fetch(this.urlSetRoleToUser, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(args),
        })
          .then(function () {
            el.dxUsersGrid.refresh();
            el.dxRolesGrid.refresh();
            el.updateRolesSelect();
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
