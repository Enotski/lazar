<template>
  <div class="container-fluid p-3">
    <DxDataGrid
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
      v-bind:filter-row="filterRow"
      v-bind:header-filter="headerFilter"
      :column-chooser="columnChooser"
      :column-resizing-mode="columnResizingMode"
      :column-width="columnWidth"
      :no-data-mess="noDataMess"
      :allow-column-reordering="allowColumnReordering"
      :allow-column-resizing="allowColumnResizing"
      v-bind:column-auto-width="columnAutoWidth"
    >
    </DxDataGrid>
  </div>
</template>
<script>
import DxDataGrid from 'devextreme-vue/data-grid';
import DataSource from "devextreme/data/data_source";
import CustomStore from "devextreme/data/custom_store";

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
  getColumnLookup: function (url, data) {
    return {
      dataSource: new CustomStore({
        loadMode: "raw",
        load: async function () {
          return await $fetch(url, {
            method: "post",
            body: { params },
          })
            .then((response) => response.res)
            .then((data) => {
              let options = [];
              data.forEach(function (item) {
                options.push(item.Text);
              });
              return options;
            })
            .catch(() => {
              throw new Error("Data Loading Error");
            });
        }.bind(this),
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
      default: () => {
        return {};
      },
    },
    height: {
      type: Number,
      default: 700,
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
  data: function () {
    return {
      dataSource: gridDataSource,
    };
  },
  beforeCreate: function(){
    console.log(this.dataUrl + '  ' + this.keyExpr);
    let key_exp = this.keyExpr;
    let data_url = this.dataUrl;
    gridDataSource = new DataSource({
        key: key_exp,
        load: async function (loadOptions) {
          console.log('load Data');
          let sorts = [];
          let filters = [];
          if (!!loadOptions["sort"]) {
            loadOptions["sort"].forEach(function (item) {
              sorts.push({
                ColumnName: item.selector,
                Type: item.desc ? 1 : 0,
              });
            });
          }
          if (!!loadOptions["filter"]) {
            if (loadOptions["filter"].hasOwnProperty("filterValue")) {
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
          var params = {
            skip: loadOptions["skip"] || 0,
            take: loadOptions["take"] || 1000,
            sorts: sorts,
            filters: filters,
          };
          let data = {};
          if (!!data) {
            switch (typeof data) {
              case "object": {
                params = Object.assign({}, params, data);
                break;
              }
              case "function": {
                params = Object.assign({}, params, data());
                break;
              }
            }
          }
          return await $fetch(data_url, {
            method: "post",
            body: { params },
          })
            .then((response) => response.res)
            .then((data) => ({
              data: data.data,
              totalCount: data.totalCount,
              summary: data.summary,
              groupCount: data.groupCount,
            }))
            .catch(() => {
              throw new Error("Data Loading Error");
            });
        },
      })
  },
  methods: {
    onInitialized: function (e) {
      if (!!events.onInitialized) {
        events.onInitialized(e);
      }
    },
    onRowClick: function (e) {
      if (!!events.onRowClick) {
        events.onRowClick(e);
      }
    },
    onRowDblClick: function (e) {
      if (!!events.onRowDblClick) {
        events.onRowDblClick(e);
      }
    },
    onSelectionChanged: function (e) {
      if (!!events.onSelectionChanged) {
        events.onSelectionChanged(e);
      }
    },
    onInitNewRow: function (e) {
      if (!!events.onInitNewRow) {
        events.onInitNewRow(e);
      }
    },
    onRowPrepared: function (e) {
      if (!!events.onRowPrepared) {
        events.onRowPrepared(e);
      }
    },
    onRowUpdated: function (e) {
      if (!!events.onRowUpdated) {
        events.onRowUpdated(e);
      }
    },
    onOptionChanged: function (e) {
      if (!!events.onOptionChanged) {
        events.onOptionChanged(e);
      }
    },
    onContextMenuPreparing: function (e) {
      if (e.target === "header") {
        if (e.row.rowType === "filter") {
          if (!e.items) e.items = [];
          e.items.push({
            text: "Сбросить фильтры",
            onItemClick: function () {
              table.clearFilter();
            },
          });
        }
        if (e.row.rowType === "header") {
          if (!e.items) e.items = [];
          e.items.push({
            text: "Выбор столбцов",
            onItemClick: function () {
              table.showColumnChooser();
            },
          });
        }
      }
    },
    onCellClick: function (e) {
      if (!!events.onCellClick) {
        events.onCellClick(e);
      }
    },
    onCellDblClick: function (e) {
      Vue.$copyTextContent(e.cellElement[0]);
      if (!!events.onCellDblClick) {
        events.onCellDblClick(e);
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
      if (!!events.onContentReady) {
        events.onContentReady(e);
      }
    },
  },
};
</script>
