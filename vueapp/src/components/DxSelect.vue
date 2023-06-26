<template>
    <DxSelectBox
      :data-source="source"
      :display-expr="displayExpr"
      :value-expr="keyExpr"
    />
</template>

<style scoped></style>

<script>
import { DxSelectBox } from "devextreme-vue/select-box";
import CustomStore from "devextreme/data/custom_store";

let source = [];

export default {
  components: {
    DxSelectBox,
  },
  props: {
    events: {
      type: Object,
      default: () => {
        return {};
      },
    },
    dataUrl: {
      type: String,
      default: "",
    },
    displayExpr: {
      type: String,
      default: "Name",
    },
    keyExpr: {
      type: String,
      default: "Id",
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
    onSelectionChanged: function (e) {
      if (
        this.events.onSelectionChanged !== undefined &&
        this.events.onSelectionChanged !== null
      ) {
        this.events.onSelectionChanged(e);
      }
    },
  },
};
</script>
