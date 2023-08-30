<template>
  <DxSelectBox
    :ref="refKey"
    :search-enabled="true"
    :data-source="store"
    :display-expr="displayExpr"
    :search-mode="searchModeOption"
    :search-expr="searchExprOption"
    :min-search-length="minSearchLengthOption"
    :show-data-before-search="showDataBeforeSearchOption"
    :search-timeout="searchTimeoutOption"
    :height="height"
    :width="width"
    :value="value"
    :disabled="disabled"
  />
</template>

<style scoped></style>

<script>
import { DxSelectBox } from "devextreme-vue/select-box";
import CustomStore from "devextreme/data/custom_store";

import {sendRequest} from "../../../utils/requestUtils";

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
    remoteSource: {
      type: Boolean,
      default: true,
    },
    localSource: {
      type: Array,
      default: () => {
        return [];
      },
    },
    paramsData: {
      type: Object,
      default: () => {
        return {};
      },
    },
    refKey: {
      type: String,
      default: "dx_select",
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
    searchEnabled: {
      type: Boolean,
      default: true,
    },
    disabled: {
      type: Boolean,
      default: false,
    },
    searchModeOption: {
      type: String,
      default: "contains",
    },
    searchExprOption: {
      type: String,
      default: "Name",
    },
    searchTimeoutOption: {
      type: Number,
      default: 0,
    },
    minSearchLengthOption: {
      type: Number,
      default: 0,
    },
    height: {
      type: Number,
      default: 34,
    },
    width: {
      type: String,
      default: "auto",
    },
    showDataBeforeSearchOption: {
      type: Boolean,
      default: false,
    },
  },
  computed: {
    dx_select: function () {
      return this.$refs[this.refKey].instance;
    },
  },
  data: function () {
    return {
      store: source,
      value: {},
    };
  },
  beforeCreate: function () {
    if (this.remoteSource) {
      let key_exp = this.keyExpr;
      let data_url = this.dataUrl;
      let params_data = this.paramsData;

      source = new CustomStore({
        key: key_exp,
        load: async function (loadOptions) {
          let args = {
            searchExpr: loadOptions.searchExpr,
            searchOperation: loadOptions.searchOperation,
            searchValue: loadOptions.searchValue,
          };
          if (params_data !== undefined && params_data !== null) {
            switch (typeof params_data) {
              case "object": {
                args = Object.assign({}, args, params_data);
                break;
              }
            }
          }
          return await sendRequest(data_url, "POST", args)
            .then((data) => data.Data)
            .catch(() => {
              throw new Error("Data Loading Error");
            });
        },
      });
    }
    else{
      source = this.localSource;
    }
  },
  methods: {
    getDxSelect: function () {
      return this.dx_select;
    },
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
