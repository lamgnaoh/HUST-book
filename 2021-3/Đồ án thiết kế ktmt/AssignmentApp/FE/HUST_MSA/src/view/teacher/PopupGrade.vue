<template>
  <!-- <div class="popup"> -->
  <!-- <div class="detail-grade"> -->
  <div class="p-fluid">
    <div class="card">
      <h5>Cell Editing</h5>
      <DataTable
        :value="products1"
        editMode="cell"
        @cell-edit-complete="onCellEditComplete"
        class="editable-cells-table"
        responsiveLayout="scroll"
      >
        <Column
          v-for="col of columns"
          :field="col.field"
          :header="col.displayName"
          :key="col.field"
          style="width: 25%"
        >
          <template #editor="{ data}">
            <InputText v-model="data[col.field]" autofocus />
          </template>
        </Column>
      </DataTable>
    </div>
  </div>
  <!-- </div>
  </div> -->
</template>
<script>
import { FilterMatchMode } from "primevue/api";
import {gradeColumns} from './columGrade.js';
import DataTable from "primevue/datatable";
import Column from "primevue/column";
import EmployeeApi from "../../api/entities/EmployeeApi";

export default {
  components: {
    DataTable,
    Column,
  },
  data() {
    return {
      editingRows: [],
      columns: gradeColumns,
      products1: null,
      products2: null,
      currentPage: 1,
      pagingSize: 100,
      products3: null,
      statuses: [
        { label: "In Stock", value: "INSTOCK" },
        { label: "Low Stock", value: "LOWSTOCK" },
        { label: "Out of Stock", value: "OUTOFSTOCK" },
      ],
      filters: {
        code: { value: null, matchMode: FilterMatchMode.STARTS_WITH },
        name: { value: null, matchMode: FilterMatchMode.STARTS_WITH },
        quantity: { value: null, matchMode: FilterMatchMode.STARTS_WITH },
        price: { value: null, matchMode: FilterMatchMode.STARTS_WITH },
      },
    };
  },
  created() {this.load();
  },
  methods: {
    getQueryStringFilter() {
      var paramStrs = `pageSize=${this.pagingSize}&pageNumber=${this.currentPage}`;
      if (this.searchTerms !== undefined && this.searchTerms !== "") {
        paramStrs += `&searchTerms=${this.searchTerms}`;
      }
      return paramStrs;
    },
    /**
     * Gọi api filter để thực hiện lấy dữ liệu đã được tìm kiếm và phân trang,
     * nhận res.data là list employee truyền cho Table và ToltalRecord để truyền cho pagingBar
     * Author TrungTQ
     * */
    load() {
      var vm = this;
      EmployeeApi.getFilterPaging(this.getQueryStringFilter()).then((res) => {
        console.log(res);
        vm.field = res.data.data.data;
      });
    },
    onCellEditComplete(event) {
      let { data, newValue, field } = event;

      switch (field) {
        case "quantity":
        case "price":
          if (this.isPositiveInteger(newValue)) data[field] = newValue;
          else event.preventDefault();
          break;

        default:
          if (newValue.trim().length > 0) data[field] = newValue;
          else event.preventDefault();
          break;
      }
    },
    isPositiveInteger(val) {
      let str = String(val);
      str = str.trim();
      if (!str) {
        return false;
      }
      str = str.replace(/^0+/, "") || "0";
      var n = Math.floor(Number(str));
      return n !== Infinity && String(n) === str && n >= 0;
    },
    onRowEditSave(event) {
      let { newData, index } = event;

      this.products2[index] = newData;
    },
    getStatusLabel(status) {
      switch (status) {
        case "INSTOCK":
          return "In Stock";

        case "LOWSTOCK":
          return "Low Stock";

        case "OUTOFSTOCK":
          return "Out of Stock";

        default:
          return "NA";
      }
    },
  },
  mounted() {
    this.load();
  },
};
</script>
  <style lang="scss" scoped>
::v-deep(.editable-cells-table td.p-cell-editing) {
  padding-top: 0;
  padding-bottom: 0;
}
//   .detail-grade {
//   width: calc(100% - 16px);
//     height: calc(100vh - 249px);
//     display: flex;
//     flex-direction: column;
//     padding: 20px 26px 0;
//     z-index: 34000;
//     position: fixed;
//     top: 78px;
//     background-color: #fff;
//     /* padding: 0 20px 20px; */
//     overflow: auto;
//     justify-content: space-between;
//     align-items: center;

// }
// .height-table{
//   height: 90%;
// }
</style>
