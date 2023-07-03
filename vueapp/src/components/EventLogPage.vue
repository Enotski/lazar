<template>
  <div class="container-fluid content-container">
    <div class="d-flex flex-row">
      <div>
        <DxGrid
          :ref="eventLogGridRef"
          :data-url="urlGetEventLog"
          :columns="eventLogColumns"
          :events="userEvents"
          :height="usersTableHeight"
          :editing="usersGridEditing"
          :data-edit-functions="usersEditFunctions"
        />
      </div>
    </div>
  </div>
</template>

<script>
import DxGrid from "./DxGrid.vue";

import {sendRequest, apiUrl} from "../../utils/requestUtils";

export default {
  components: {
    DxGrid
  },
  computed: {
    dxEventLogGrid: function () {
      return this.$refs[this.eventLogGridRef].getDxGrid();
    },
  },
  data() {
    return {
      urlGetUsers: `${apiUrl}/Users/GetUsersDataGrid`,
      urlGetRoles: `${apiUrl}/Roles/GetRolesDataGrid`,
      urlGetRolesList: `${apiUrl}/Roles/GetRoles`,
      urlSetRoleToUser: `${apiUrl}/UserProfile/SetRoleToUser`,
      eventLogGridRef: "event_log_grid",
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
          await sendRequest(`${apiUrl}/Users/AddUser`, "POST", values),
        update: async (key, values) =>
          await sendRequest(`${apiUrl}/Users/UpdateUser`, "POST", {
            id: key,
            email: values.Email,
            login: values.Login,
          }),
        remove: async (key) =>
          await sendRequest(`${apiUrl}/Users/DeleteUser`, "POST", {
            id: key,
          }),
      },
      rolesEditFunctions: {
        insert: async (values) =>
          await sendRequest(`${apiUrl}/Roles/AddRole`, "POST", values).then(() => {
            this.updateRolesSelect();
          }),
        update: async (key, values) =>
          await sendRequest(`${apiUrl}/Roles/UpdateRole`, "POST", {
            id: key,
            name: values.Name,
          }).then(() => {
            this.dxUsersGrid.refresh();
            this.updateRolesSelect();
          }),
        remove: async (key) => {
          if (this.paramsData.selectedUserId === "")
            await sendRequest(`${apiUrl}/Roles/DeleteRole`, "POST", {
              id: key,
            }).then(() => {
              this.dxUsersGrid.refresh();
              this.updateRolesSelect();
            });
          else
            await sendRequest(`${apiUrl}/Users/RemoveRoleFromUser`, "POST", {
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
