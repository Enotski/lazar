<template>
  <div class="container-fluid p-3">
    <DxAutocomplete
      :data-source="store"
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
    </DxAutocomplete>
  </div>
</template>

<style scoped>
</style>

<script>
import { DxAutocomplete } from 'devextreme-vue/autocomplete';
import CustomStore from "devextreme/data/custom_store";

let source = [];

export default {
  components: {
    DxAutocomplete,
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
    selection: {
      type: Object,
      default: () => {
        return {
          mode: "multiple",
          showCheckBoxesMode: "none",
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
  },
  data: function () {
    return {
      store: source,
    };
  },
  beforeCreate: function () {
    let key_exp = this.keyExpr;
    let data_url = this.dataUrl;

    source = new CustomStore({
      key: key_exp,
      load: async function (loadOptions) {
        console.log("load Data");
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
        let data = {};
        if (data !== undefined && data !== null) {
          switch (typeof data) {
            case "object": {
              args = Object.assign({}, args, data);
              break;
            }
            case "function": {
              args = Object.assign({}, args, data());
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