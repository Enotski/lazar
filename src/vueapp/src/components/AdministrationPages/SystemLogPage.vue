<template>
  <div class="container-fluid content-container">
    <div class="d-flex flex-row mb-3">
      <DxDateRangeBox
        :ref="dateRangeRef"
        :show-clear-button="true"
        :value="initialValue"
        display-format="dd.MM.yyyy"
      />
      <div class="ms-5 pt-2">
        <n-button @click="clearEventLog" type="error" ghost >Remove logs</n-button>
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
import { NButton } from "naive-ui";

import { DataGrid } from "../../../utils/DxGridUtils";
import { sendRequest, apiUrl } from "../../../utils/requestUtils";

const msInDay = 1000 * 60 * 60 * 24;
const now = new Date();
const subSystemTypeUrl = `${apiUrl}/types/get-subsystem-type`;
const eventTypeUrl = `${apiUrl}/types/get-event-types`;

export default {
  components: {
    DxGrid,
    DxDateRangeBox,
    NButton,
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
        new Date(now.getTime() - msInDay * 3),
        new Date(now.getTime() + msInDay * 3),
      ],
      getEventLogUrl: `${apiUrl}/event-logs/get-data-grid`,
      removeEventLogByPeriodUrl: `${apiUrl}/event-logs/remove-by-period`,
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
          lookup: DataGrid.getColumnLookup(subSystemTypeUrl, "GET"),
        },
        {
          caption: "Тип",
          dataField: "EventTypeName",
          filterOperations: ["=", "<>"],
          lookup: DataGrid.getColumnLookup(eventTypeUrl, "GET"),
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
  mounted: function () {},
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
  },
};
</script>
<style></style>
