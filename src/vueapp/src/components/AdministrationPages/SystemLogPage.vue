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
        :ref="systemLogGridRef"
        :data-url="getEventLogUrl"
        :columns="systemLogColumns"
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
const subSystemTypeUrl = `${apiUrl}/types/get-subsystem-types`;
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
      return this.$refs[this.systemLogGridRef].getDxGrid();
    },
  },
  data() {
    return {
      initialValue: [
        new Date(now.getTime() - msInDay * 3),
        new Date(now.getTime() + msInDay * 3),
      ],
      getEventLogUrl: `${apiUrl}/system-log/get-all`,
      removeSystemLogByPeriodUrl: `${apiUrl}/system-log/remove-by-period`,
      systemLogGridRef: "system_log_grid",
      dateRangeRef: "log_period",
      systemLogColumns: [
        {
          dataField: "Num",
          allowSorting: false,
          allowFiltering: false,
          width: 60,
        },
        {
          caption: "Subsystem",
          dataField: "SubSystemName",
          alignment: "center",
          filterOperations: ["=", "<>"],
          lookup: DataGrid.getColumnLookup(subSystemTypeUrl, "GET"),
        },
        {
          caption: "Event", 
          dataField: "EventTypeName",
          filterOperations: ["=", "<>"],
          lookup: DataGrid.getColumnLookup(eventTypeUrl, "GET"),
        },
        {
          dataField: "Description",
        },
        {
          caption: "Initiator",
          dataField: "ChangedBy",
        },
        {
          caption: "Date",
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
        startDate: value[0].toLocaleString('ru-Ru'),
        endDate: value[1].toLocaleString('ru-Ru'),
      };
      await sendRequest(this.removeSystemLogByPeriodUrl, "POST", period).then(
        () => {
          el.dxEventLogGrid.refresh();
        }
      );
    },
  },
};
</script>
<style></style>
