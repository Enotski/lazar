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
@import "../css/custom-dx.css";
</style>

<script>
import DxDataGrid from "devextreme-vue/data-grid";
import CustomStore from "devextreme/data/custom_store";

import moment from "moment";

export const DataGrid = {
  getFilterType: function (filterOperation) {
    var type;
    switch (filterOperation) {
      case "contains": {
        type = 0;
        break;
      }
      case "notcontains": {
        type = 1;
        break;
      }
      case "startswith": {
        type = 2;
        break;
      }
      case "endswith": {
        type = 3;
        break;
      }
      case "=": {
        type = 4;
        break;
      }
      case "<>": {
        type = 5;
        break;
      }
      case "<": {
        type = 6;
        break;
      }
      case ">": {
        type = 7;
        break;
      }
      case "<=": {
        type = 8;
        break;
      }
      case ">=": {
        type = 9;
        break;
      }
      case "between": {
        type = 10;
        break;
      }
    }
    return type;
  },
  getFullDateTimeFormat: function () {
    return "dd.MM.yyyy HH:mm:ss";
  },
  getShortDateTimeFormat: function () {
    return "dd.MM.yyyy";
  },
  getCalculateFilterExpression: function (
    filterValue,
    selectedFilterOperation
  ) {
    if (this.dataType === "date" || this.dataType === "datetime") {
      switch (selectedFilterOperation) {
        case "<>": {
          return [
            this.dataField,
            "<>",
            moment(filterValue).format("DD.MM.YYYY HH:mm:ss"),
          ];
        }
        case "=": {
          return [
            this.dataField,
            "=",
            moment(filterValue).format("DD.MM.YYYY HH:mm:ss"),
          ];
        }
        case ">": {
          return [
            this.dataField,
            ">",
            moment(filterValue).format("DD.MM.YYYY HH:mm:ss"),
          ];
        }
        case "<": {
          return [
            this.dataField,
            "<",
            moment(filterValue).format("DD.MM.YYYY HH:mm:ss"),
          ];
        }
        case ">=": {
          return [
            this.dataField,
            ">=",
            moment(filterValue).format("DD.MM.YYYY HH:mm:ss"),
          ];
        }
        case "<=": {
          return [
            this.dataField,
            "<=",
            moment(filterValue).format("DD.MM.YYYY HH:mm:ss"),
          ];
        }
        case "between": {
          return [
            this.dataField,
            "between",
            moment(filterValue[0]).format("DD.MM.YYYY HH:mm:ss") +
              ";" +
              moment(filterValue[1]).format("DD.MM.YYYY HH:mm:ss"),
          ];
        }
      }
    } else if (this.dataType === "number") {
      switch (selectedFilterOperation) {
        case "<>": {
          return [this.dataField, "<>", filterValue];
        }
        case "=": {
          return [this.dataField, "=", filterValue];
        }
        case ">": {
          return [this.dataField, ">", filterValue];
        }
        case "<": {
          return [this.dataField, "<", filterValue];
        }
        case ">=": {
          return [this.dataField, ">=", filterValue];
        }
        case "<=": {
          return [this.dataField, "<=", filterValue];
        }
        case "between": {
          return [
            this.dataField,
            "between",
            filterValue[0] + ";" + filterValue[1],
          ];
        }
      }
    }
    return this.defaultCalculateFilterExpression.apply(this, arguments);
  },
  getColumnLookup: function (url, params) {
    return {
      dataSource: new CustomStore({
        load: async function () {
          return await fetch(url, {
            method: "POST",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify(params),
          })
            .then((response) => response.json())
            .then(async function (data) {
              let options = [];
              data.forEach(function (item) {
                options.push(item.Text);
              });
              return options;
            })
            .catch(() => {
              throw new Error("Data Loading Error");
            });
        },
      }),
    };
  },
};

let gridDataSource = [];

export default {
  components: {
    DxDataGrid,
  },
  props: {
    events: {
      type: Object,
      default: () => { return {}},
    },
    paramsData:{
      type: Object,
      default: () => { return {}}
    },
    height: {
      type: Number,
      default: 700,
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
  },
  computed: {
        dx_grid: function() {
            return this.$refs[this.refKey].instance;
        }
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
    
    gridDataSource = new CustomStore({
      key: key_exp,
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
        return await fetch(data_url, {
          method: "POST",
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(args),
        })
          .then((response) => response.json())
          .then(async function (data) {
            return {
              data: data.data,
              totalCount: data.totalCount,
              summary: data.summary,
              groupCount: data.groupCount,
            };
          })
          .catch(() => {
            throw new Error("Data Loading Error");
          });
      },
    });
  },
  methods: {
    getDxGrid: function(){
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
