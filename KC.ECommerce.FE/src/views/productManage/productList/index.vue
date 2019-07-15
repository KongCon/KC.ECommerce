<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input v-model="listQuery.name" placeholder="请输入产品名称" style="width: 160px;" class="filter-item" @keyup.enter.native="handleFilter" />
      <el-select v-model="listQuery.status" placeholder="产品状态" clearable style="width: 120px" class="filter-item">
        <el-option v-for="item in statusOptions" :key="item.key" :label="item.label" :value="item.key" />
      </el-select>
      <el-button v-waves class="filter-item" type="primary" icon="el-icon-search" @click="handleFilter">查询</el-button>
    </div>

    <el-table :key="tableKey" v-loading="listLoading" :data="list" :height="tableHeight" border fit highlight-current-row style="width: 100%">
      <el-table-column type="expand" label="#">
        <template slot-scope="scope">
          <el-form label-position="left" inline class="table-expand">
            <el-form-item label="简述">
              <span>{{ scope.row.description }}</span>
            </el-form-item>
          </el-form>
        </template>
      </el-table-column>
      <el-table-column type="index" label="序号" width="50" align="center"></el-table-column>
      <el-table-column label="ID" prop="id" align="center" width="80">
        <template slot-scope="scope">
          <span>{{ scope.row.id }}</span>
        </template>
      </el-table-column>
      <el-table-column label="产品名称" align="center">
        <template slot-scope="scope">
          <span>{{ scope.row.name }}</span>
        </template>
      </el-table-column>
      <el-table-column label="产品状态" align="center" width="80">
        <template slot-scope="scope">
          <span>{{ scope.row.status }}</span>
        </template>
      </el-table-column>
      <el-table-column label="序号" align="center" width="80">
        <template slot-scope="scope">
          <span>{{ scope.row.sequence }}</span>
        </template>
      </el-table-column>
      <el-table-column label="创建日期" align="center" width="135">
        <template slot-scope="scope">
          <span>{{ scope.row.createdDate }}</span>
        </template>
      </el-table-column>
      <el-table-column label="更新日期" align="center" width="135">
        <template slot-scope="scope">
          <span>{{ scope.row.updatedDate }}</span>
        </template>
      </el-table-column>
      <el-table-column fixed="right" label="操作" align="center" width="120" class-name="small-padding fixed-width">
        <template slot-scope="{row}">
          <el-button type="primary" v-if="row.handleStatus==='未处理'" size="mini">编辑</el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total>0" :total="total" :page.sync="listQuery.page" :limit.sync="listQuery.limit" @pagination="getList" />
  </div>
</template>

<style>
.table-expand {
  font-size: 0;
}
.table-expand label {
  width: 90px;
  color: #99a9bf;
}
.table-expand .el-form-item {
  margin-right: 0;
  margin-bottom: 0;
  width: 100%;
}

.el-form-item__content {
  padding-right: 100px;
  width: 100%;
}
</style>
<script>
import { getList } from "@/api/productManage";
import waves from "@/directive/waves"; // waves directive
import { parseTime } from "@/utils";
import Pagination from "@/components/Pagination"; // secondary package based on el-pagination

export default {
  name: "productList",
  components: { Pagination },
  directives: { waves },
  data() {
    return {
      tableHeight: window.innerHeight - 210,
      tableKey: 0,
      list: null,
      total: 0,
      listLoading: true,
      listQuery: {
        page: 1,
        limit: 20,
        name: undefined,
        status: "All"
      },
      statusOptions: [
        { label: "所有", key: "All" },
        { label: "就绪", key: "Ready" },
        { label: "已上架", key: "OnShelf" },
        { label: "已下架", key: "Dismount" }
      ]
    };
  },
  created() {
    this.getList();
  },
  methods: {
    getList() {
      if (Number(this.listQuery.id)) {
        this.listQuery.id = Number(this.listQuery.id);
      } else {
        this.listQuery.id = undefined;
      }
      this.listLoading = true;
      getList(this.listQuery).then(response => {
        this.list = response.data.items;
        this.total = response.data.total;
        this.listLoading = false;
        // Just to simulate the time of the request
        // setTimeout(() => {
        //   this.listLoading = false;
        // }, 1 * 1000);
      });
    },
    handleFilter() {
      this.listQuery.page = 1;
      this.getList();
    }
  }
};
</script>
