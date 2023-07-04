<template>
  <div class="container-fluid content-container">
    <div class="d-flex flex-row mb-3">
      <DxDateRangeBox
        :ref="dateRangeRef"
        :show-clear-button="true"
        :value="initialValue"
        display-format="dd.MM.yyyy"
      />
      <div class="pt-2">
        <v-btn color="danger" variant="text" @click="clearEventLog"
          >Remove logs</v-btn
        >
      </div>
    </div>
    <div class="d-flex flex-row">
      <DxGrid
        :ref="eventLogGridRef"
        :data-url="getEventLogUrl"
        :columns="eventLogColumns"
        :height="eventLogGridHeight"
      />
    </div>
  </div>
</template>

<script>
import DxGrid from "../DxComponents/DxGrid.vue";
import DxDateRangeBox from "devextreme-vue/date-range-box";

import { DataGrid } from "../../../utils/DxGridHelpers";
import { sendRequest, apiUrl } from "../../../utils/requestUtils";
import CustomStore from "devextreme/data/custom_store";

const msInDay = 1000 * 60 * 60 * 24;
const now = new Date();
const subSystemTypeUrl = `${apiUrl}/types/get-subsystem-type`;
const eventTypeUrl = `${apiUrl}/types/get-event-types`;
let eventTypeStore = [];
let subSystemTypeStore = [];
export default {
  components: {
    DxGrid,
    DxDateRangeBox,
  },
  computed: {
    dxDateRange: function () {
      return this.$refs[this.dateRangeRef].instance;
    },
    dxEventLogGrid: function () {
      return this.$refs[this.eventLogGridRef].getDxGrid();
    },
  },
  data() {
    return {
      initialValue: [
        new Date(now.getTime() - msInDay * 3).toLocaleDateString(),
        new Date(now.getTime() + msInDay * 3).toLocaleDateString(),
      ],
      getEventLogUrl: `${apiUrl}/event-logs/get-event-logs-data-grid`,
      removeEventLogByPeriodUrl: `${apiUrl}/event-logs/remove-logs-by-period`,
      eventLogGridRef: "event_log_grid",
      dateRangeRef: "log_period",
      eventLogGridHeight: 500,
      eventLogColumns: [
        {
          caption: "№ п/п",
          dataField: "Num",
          allowSorting: false,
          allowFiltering: false,
          width: 60,
        },
        {
          caption: "Подсистема",
          dataField: "SubSystemName",
          alignment: "center",
          filterOperations: ["=", "<>"],
          // lookup: {
          //   dataSource: subSystemTypeStore,
          //   valueExpr: "Key",
          //   displayExpr: "Text",
          // },
        },
        {
          caption: "Тип",
          dataField: "EventTypeName",
          alignment: "center",
          filterOperations: ["=", "<>"],
          lookup: new CustomStore({
            load: async function () {
              await sendRequest(`${apiUrl}/types/get-event-types`, "GET")
                .then((data) => data)
                .catch(() => {
                  throw new Error("Data Loading Error");
                });
            },
          }),
        },
        {
          caption: "Пользователь",
          dataField: "UserName",
        },
        {
          caption: "Дата Создания",
          dataField: "DateChange",
          dataType: "date",
          format: DataGrid.getFullDateTimeFormat(),
          calculateFilterExpression: DataGrid.getCalculateFilterExpression,
        },
      ],
    };
  },
  mounted: async function () {
    // await this.getEventType();
    // await this.getSubSystemTypes();
  },
  methods: {
    clearEventLog: async function () {
      let el = this;
      let value = this.dxDateRange.option("value");
      let period = {
        startDate: value[0],
        endDate: value[1],
      };
      await sendRequest(this.removeEventLogByPeriodUrl, "POST", period).then(
        () => {
          el.dxEventLogGrid.refresh();
        }
      );
    },
    getEventType: async function () {
      if (eventTypeStore.length === 0) {
        eventTypeStore = await sendRequest(eventTypeUrl, "GET")
          .then((data) => data)
          .catch(() => {
            throw new Error("Data Loading Error");
          });
      }
    },
    getSubSystemTypes: async function () {
      if (subSystemTypeStore.length === 0) {
        subSystemTypeStore = await sendRequest(subSystemTypeUrl, "GET")
          .then((data) => data)
          .catch(() => {
            throw new Error("Data Loading Error");
          });
      }
    },
  },
};
</script>
<style></style>
