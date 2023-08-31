<template>
  <div class="container-fluid content-container">
    <div class="row">
      <div class="col-6 d-flex mb-3">
            <div class="col-auto me-3">
              <n-button
                @click="setRoleToUser"
                type="success"
                ghost
                icon-placement="left"
              >
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
    </div>
    <div class="row">
      <div class="col-6">
        <DxGrid
            :ref="usersGridRef"
            :data-url="urlGetUsers"
            :columns="userColumns"
            :events="userEvents"
            :editing="usersGridEditing"
            width="100%"
            :data-edit-functions="usersEditFunctions"
          />
      </div>
      <div class="col-6 pt-0">
            <DxGrid
              :ref="rolesGridRef"
              :data-url="urlGetRoles"
              :columns="roleColumns"
              :params-data="paramsData"
              :editing="rolesGridEditing"
              width="100%"
              :data-edit-functions="rolesEditFunctions"
            />
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
      urlGetUsers: `${apiUrl}/users/get-all`,
      urlGetRoles: `${apiUrl}/roles/get-by-user`,
      urlGetRolesList: `${apiUrl}/roles/get-list-by-user`,
      urlSetRoleToUser: `${apiUrl}/users/set-role`,
      usersGridRef: "users_grid",
      rolesGridRef: "roles_grid",
      rolesSelectRef: "roles_select",
      selectDisabled: true,
      paramsData: {
        selectedUserId: "",
      },
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
        allowAdding: false,
        allowUpdating: false,
        allowDeleting: true,
        confirmDelete: true,
        useIcons: true,
        mode: "row",
        refreshMode: "full",
      },
      usersEditFunctions: {
        remove: async (key) =>
          await sendRequest(`${apiUrl}/users/delete`, "POST", 
            [key]
          ),
      },
      rolesEditFunctions: {
        insert: async (values) =>
          await sendRequest(`${apiUrl}/roles/create`, "POST", values).then(
            () => {
              this.updateRolesSelect();
            }
          ),
        update: async (key, values) =>
          await sendRequest(`${apiUrl}/roles/update`, "POST", {
            id: key,
            name: values.Name,
          }).then(() => {
            this.dxUsersGrid.refresh();
            this.updateRolesSelect();
          }),
        remove: async (key) => {
          if (this.paramsData.selectedUserId === "")
            await sendRequest(`${apiUrl}/roles/delete`, "POST", 
              [key]
            ).then(() => {
              this.dxUsersGrid.refresh();
              this.updateRolesSelect();
            });
          else
            await sendRequest(`${apiUrl}/users/remove-role`, "POST", {
              userId: this.paramsData.selectedUserId,
              roleId: key,
            }).then(() => {
              this.dxUsersGrid.refresh();
              this.updateRolesSelect();
            });
        },
      },
      userColumns: [
        {
          dataField: "Num",
          allowSorting: false,
          allowFiltering: false,
          allowEditing: false,
          width: 60,
        },
        {
          dataField: "Login",
        },
        {
          dataField: "Email",
        },
        {
          dataField: "Roles",
          allowEditing: false
        },
      ],
      roleColumns: [
        {
          dataField: "Num",
          allowSorting: false,
          allowFiltering: false,
          allowEditing: false,
          width: 60,
        },
        {
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
          userId: this.paramsData.selectedUserId,
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
