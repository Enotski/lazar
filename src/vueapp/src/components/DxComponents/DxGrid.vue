<template>
  <DxDataGrid
    :ref="refKey"
    :data-source="dataSource"
    :columns="columns"
    :remote-operations="remoteOperations"
    :column-auto-width="columnAutoWidth"
    :filter-row="filterRow"
    :header-filter="headerFilter"
    :pager="pager"
    :selection="selection"
    :height="height"
    :width="width"
    :paging="paging"
    :key-expr="keyExpr"
    :show-column-lines="showColumnLines"
    :show-row-lines="showRowLines"
    :row-alternation-enabled="rowAlternationEnabled"
    :show-borders="showBorders"
    :column-chooser="columnChooser"
    :column-resizing-mode="columnResizingMode"
    :column-width="columnWidth"
    :no-data-mess="noDataMess"
    :allow-column-reordering="allowColumnReordering"
    :allow-column-resizing="allowColumnResizing"
    :editing="editing"
    v-on:initialized="onInitialized"
    v-on:row-click="onRowClick"
    v-on:row-dbl-click="onRowDblClick"
    v-on:selection-changed="onSelectionChanged"
    v-on:init-new-row="onInitNewRow"
    v-on:row-prepared="onRowPrepared"
    v-on:row-updated="onRowUpdated"
    v-on:option-changed="onOptionChanged"
    v-on:context-menu-preparing="onContextMenuPreparing"
    v-on:cell-click="onCellClick"
    v-on:content-ready="onContentReady"
  >
  </DxDataGrid>
</template>

<style scoped>
@import "../../css/custom-dx.css";
</style>

<script>
import DxDataGrid from "devextreme-vue/data-grid";
import CustomStore from "devextreme/data/custom_store";

import { sendRequest } from "@/utils/requestUtils";
import { DataGrid } from "@/utils/DxGridUtils";

let gridDataSource = [];

export default {
  components: {
    DxDataGrid,
  },
  props: {
    events: {
      type: Object,
      default: () => {
        return {};
      },
    },
    paramsData: {
      type: Object,
      default: () => {
        return {};
      },
    },
    height: {
      type: Number,
      default: 700,
    },
    width: {
      type: Number,
      default: null,
    },
    refKey: {
      type: String,
      default: "dx_grid",
    },
    dataUrl: {
      type: String,
      default: "",
    },
    keyExpr: {
      type: String,
      default: "Id",
    },
    showColumnLines: {
      type: Boolean,
      default: true,
    },
    showRowLines: {
      type: Boolean,
      default: true,
    },
    rowAlternationEnabled: {
      type: Boolean,
      default: true,
    },
    showBorders: {
      type: Boolean,
      default: true,
    },
    paging: {
      type: Object,
      default: () => {
        return {
          pageSize: 1000,
          pageIndex: 0,
          enabled: true,
        };
      },
    },
    filterRow: {
      type: Object,
      default: () => {
        return {
          visible: true,
          applyFilter: "auto",
        };
      },
    },
    headerFilter: {
      type: Object,
      default: () => {
        return {
          visible: false,
        };
      },
    },
    pager: {
      type: Object,
      default: () => {
        return {
          showPageSizeSelector: true,
          allowedPageSizes: [50, 100, 200, 1000],
          showNavigationButtons: true,
          visible: true,
        };
      },
    },
    selection: {
      type: Object,
      default: () => {
        return {
          mode: "multiple",
          showCheckBoxesMode: "none",
        };
      },
    },
    columnChooser: {
      type: Object,
      default: () => {
        return {
          enabled: false,
          mode: "select",
        };
      },
    },
    sorting: {
      type: Object,
      default: () => {
        return {
          mode: "multiple",
        };
      },
    },
    columnWidth: {
      type: Number,
      default: 200,
    },
    columnResizingMode: {
      type: String,
      default: "widget",
    },
    noDataMess: {
      type: String,
      default: "",
    },
    remoteOperations: {
      type: Boolean,
      default: true,
    },
    allowColumnReordering: {
      type: Boolean,
      default: true,
    },
    allowColumnResizing: {
      type: Boolean,
      default: true,
    },
    columnAutoWidth: {
      type: Boolean,
      default: true,
    },
    columns: {
      type: Array,
      default: () => [],
    },
    editing: {
      type: Object,
      default: () => {
        return {
          allowAdding: false,
          allowUpdating: false,
          allowDeleting: false,
          confirmDelete: false,
          useIcons: true,
          mode: "row",
          refreshMode: "full",
        };
      },
    },
    dataEditFunctions: {
      type: Object,
      default: () => {
        return {
          insert: null,
          update: null,
          remove: null,
        };
      },
    },
  },
  computed: {
    dx_grid: function () {
      return this.$refs[this.refKey].instance;
    },
  },
  data: function () {
    return {
      dataSource: gridDataSource,
    };
  },
  beforeCreate: function () {
    let key_exp = this.keyExpr;
    let data_url = this.dataUrl;
    let params_data = this.paramsData;
    let data_edit_functions = this.dataEditFunctions;

    gridDataSource = new CustomStore({
      key: key_exp,
      insert: data_edit_functions?.insert,
      update: data_edit_functions?.update,
      remove: data_edit_functions?.remove,
      load: async function (loadOptions) {
        let sorts = [];
        let filters = [];
        if (loadOptions["sort"] !== undefined && loadOptions["sort"] !== null) {
          loadOptions["sort"].forEach(function (item) {
            sorts.push({
              ColumnName: item.selector,
              Type: item.desc ? 1 : 0,
            });
          });
        }
        if (
          loadOptions["filter"] !== undefined &&
          loadOptions["filter"] !== null
        ) {
          if (
            Object.prototype.hasOwnProperty.call(
              loadOptions["filter"],
              "filterValue"
            )
          ) {
            filters.push({
              ColumnName: loadOptions["filter"][0],
              Type: DataGrid.getFilterType(loadOptions["filter"][1]),
              Value: loadOptions["filter"][2],
            });
          } else {
            loadOptions["filter"]
              .filter(function (item) {
                return item !== "and" && item !== "or";
              })
              .forEach(function (item) {
                filters.push({
                  ColumnName: item[0],
                  Type: DataGrid.getFilterType(item[1]),
                  value: item[2],
                });
              });
          }
        }
        var args = {
          skip: loadOptions["skip"] || 0,
          take: loadOptions["take"] || 1000,
          sorts: sorts,
          filters: filters,
        };
        if (params_data !== undefined && params_data !== null) {
          switch (typeof params_data) {
            case "object": {
              args = Object.assign({}, args, params_data);
              break;
            }
          }
        }
        return await sendRequest(data_url, "POST", args).then(async function (
          data
        ) {
          return {
            data: data.data,
            totalCount: data.totalCount,
            summary: data.summary,
            groupCount: data.groupCount,
          };
        });
      },
    });
  },
  methods: {
    getDxGrid: function () {
      return this.dx_grid;
    },
    onInitialized: function (e) {
      if (
        this.events.onInitialized !== undefined &&
        this.events.onInitialized !== null
      ) {
        this.events.onInitialized(e);
      }
    },
    onRowClick: function (e) {
      if (
        this.events.onRowClick !== undefined &&
        this.events.onRowClick !== null
      ) {
        this.events.onRowClick(e);
      }
    },
    onRowDblClick: function (e) {
      if (
        this.events.onRowDblClick !== undefined &&
        this.events.onRowDblClick !== null
      ) {
        this.events.onRowDblClick(e);
      }
    },
    onSelectionChanged: function (e) {
      if (
        this.events.onSelectionChanged !== undefined &&
        this.events.onSelectionChanged !== null
      ) {
        this.events.onSelectionChanged(e);
      }
    },
    onInitNewRow: function (e) {
      if (
        this.events.onInitNewRow !== undefined &&
        this.events.onInitNewRow !== null
      ) {
        this.events.onInitNewRow(e);
      }
    },
    onRowPrepared: function (e) {
      if (
        this.events.onRowPrepared !== undefined &&
        this.events.onRowPrepared !== null
      ) {
        this.events.onRowPrepared(e);
      }
    },
    onRowUpdated: function (e) {
      if (
        this.events.onRowUpdated !== undefined &&
        this.events.onRowUpdated !== null
      ) {
        this.events.onRowUpdated(e);
      }
    },
    onOptionChanged: function (e) {
      if (
        this.events.onOptionChanged != undefined &&
        this.events.onOptionChanged !== null
      ) {
        this.events.onOptionChanged(e);
      }
    },
    onContextMenuPreparing: function (e) {
      if (e.target === "header") {
        if (e.row.rowType === "filter") {
          if (!e.items) e.items = [];
          e.items.push({
            text: "Сбросить фильтры",
            onItemClick: function () {
              this.table.clearFilter();
            },
          });
        }
        if (e.row.rowType === "header") {
          if (!e.items) e.items = [];
          e.items.push({
            text: "Выбор столбцов",
            onItemClick: function () {
              this.table.showColumnChooser();
            },
          });
        }
      }
    },
    onCellClick: function (e) {
      if (
        this.events.onCellClick !== undefined &&
        this.events.onCellClick !== null
      ) {
        this.events.onCellClick(e);
      }
    },
    onContentReady: function (e) {
      if (e.component.totalCount() == 0) {
        var filters = e.component.getCombinedFilter();
        if (filters) {
          e.component.option(
            "noDataText",
            "Записей, удовлетворяющих условиям поиска, не найдено. Для отображения всех записей необходимо убрать фильтр"
          );
        } else {
          e.component.option("noDataText", this.noDataMess);
        }
      }
      var columnChooserView = e.component.getView("columnChooserView");
      if (!columnChooserView._popupContainer) {
        columnChooserView._initializePopupContainer();
        columnChooserView.render();
        columnChooserView._popupContainer.option("position", {
          of: e.element,
          my: "center",
          at: "center",
        });
      }
      if (
        this.events.onContentReady !== undefined &&
        this.events.onContentReady !== null
      ) {
        this.events.onContentReady(e);
      }
    },
  },
};
</script>
