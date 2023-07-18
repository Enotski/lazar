<template>
  <div class="container-fluid content-container">
    <div class="row">
      <div class="col">
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
      <div class="col">
        <div class="row ml-5 pt-0">
          <div class="row mb-3">
            <div class="col-auto">
              <n-button @click="setRoleToUser = true" type="success" ghost  icon-placement="left">
                <template #icon>
                  <n-icon><add-icon /></n-icon>
                </template>
                Set Role
              </n-button>
            </div>
            <div class="col">
              <DxSelect
                :ref="rolesSelectRef"
                :data-url="urlGetRolesList"
                :params-data="paramsData"
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
  </div>
</template>

<script>
import DxGrid from "../DxComponents/DxGrid.vue";
import DxSelect from "../DxComponents/DxSelect.vue";
import { NButton, NIcon } from "naive-ui";
import { AddAlt as AddIcon } from "@vicons/carbon";
import { sendRequest, apiUrl } from "../../../utils/requestUtils";

export default {
  components: {
    DxGrid,
    DxSelect,
    NButton,
    NIcon,
    AddIcon,
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
      urlGetUsers: `${apiUrl}/users/get-users-data-grid`,
      urlGetRoles: `${apiUrl}/roles/get-data-grid`,
      urlGetRolesList: `${apiUrl}/roles/get-roles`,
      urlSetRoleToUser: `${apiUrl}/user-profile/set-role-to-user`,
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
          await sendRequest(`${apiUrl}/users/add-user`, "POST", values),
        update: async (key, values) =>
          await sendRequest(`${apiUrl}/users/update-user`, "POST", {
            id: key,
            email: values.Email,
            login: values.Login,
          }),
        remove: async (key) =>
          await sendRequest(`${apiUrl}/users/delete-user`, "POST", {
            id: key,
          }),
      },
      rolesEditFunctions: {
        insert: async (values) =>
          await sendRequest(`${apiUrl}/roles/add-role`, "POST", values).then(
            () => {
              this.updateRolesSelect();
            }
          ),
        update: async (key, values) =>
          await sendRequest(`${apiUrl}/roles/update-role`, "POST", {
            id: key,
            name: values.Name,
          }).then(() => {
            this.dxUsersGrid.refresh();
            this.updateRolesSelect();
          }),
        remove: async (key) => {
          if (this.paramsData.selectedUserId === "")
            await sendRequest(`${apiUrl}/roles/delete-role`, "POST", {
              id: key,
            }).then(() => {
              this.dxUsersGrid.refresh();
              this.updateRolesSelect();
            });
          else
            await sendRequest(`${apiUrl}/users/remove-role-from-user`, "POST", {
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
        await sendRequest(this.urlSetRoleToUser, "POST", args)
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
