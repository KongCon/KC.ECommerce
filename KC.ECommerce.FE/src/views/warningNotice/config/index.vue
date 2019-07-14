<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input
        v-model="listQuery.type"
        placeholder="请输入类型"
        style="width: 200px;"
        class="filter-item"
        @keyup.enter.native="handleFilter"
      />
      <el-input
        v-model="listQuery.typeDescription"
        placeholder="请输入类型描述"
        style="width: 200px;"
        class="filter-item"
        @keyup.enter.native="handleFilter"
      />
      <el-input
        v-model="listQuery.smsAccounts"
        placeholder="请输入短信账号"
        style="width: 200px;"
        class="filter-item"
        @keyup.enter.native="handleFilter"
      />
      <el-input
        v-model="listQuery.emailAccounts"
        placeholder="请输入邮箱账号"
        style="width: 200px;"
        class="filter-item"
        @keyup.enter.native="handleFilter"
      />

      <el-button
        v-waves
        class="filter-item"
        type="primary"
        icon="el-icon-search"
        @click="handleFilter"
      >查询</el-button>

      <el-button
        class="filter-item"
        style="margin-left: 10px;"
        type="success"
        icon="el-icon-edit"
        @click="handleCreate"
      >添加</el-button>
    </div>

    <el-table
      :key="tableKey"
      v-loading="listLoading"
      :data="list"
      :height="tableHeight"
      border
      fit
      highlight-current-row
      style="width: 100%"
    >
      <el-table-column type="expand" label="#">
        <template slot-scope="scope">
          <el-form label-position="left" inline class="table-expand">
            <el-form-item label="类型描述">
              <span>{{ scope.row.typeDescription }}</span>
            </el-form-item>
            <el-form-item label="默认短信内容">
              <span>{{ scope.row.defaultSmsContent }}</span>
            </el-form-item>
            <el-form-item label="默认邮件内容">
              <el-input
                type="textarea"
                :autosize="{ minRows: 2, maxRows: 5}"
                v-model="scope.row.defaultEmailContent"
              ></el-input>
            </el-form-item>
            <el-form-item label="SQL脚本">
              <el-input
                type="textarea"
                :autosize="{ minRows: 2, maxRows: 5}"
                v-model="scope.row.sqlScript"
              ></el-input>
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
      <el-table-column label="类型" align="center" width="180" show-overflow-tooltip>
        <template slot-scope="scope">
          <span>{{ scope.row.type }}</span>
        </template>
      </el-table-column>
      <el-table-column label="短信账号" align="center" show-overflow-tooltip>
        <template slot-scope="scope">
          <span>{{ scope.row.smsAccounts }}</span>
        </template>
      </el-table-column>
      <el-table-column label="邮箱账号" align="center" show-overflow-tooltip>
        <template slot-scope="scope">
          <span>{{ scope.row.emailAccounts }}</span>
        </template>
      </el-table-column>
      <el-table-column label="创建时间" align="center" width="135">
        <template slot-scope="scope">
          <span>{{ scope.row.createTime }}</span>
        </template>
      </el-table-column>
      <el-table-column
        fixed="right"
        label="操作"
        align="center"
        width="150"
        class-name="small-padding fixed-width"
      >
        <template slot-scope="{row}">
          <el-button type="primary" size="mini" @click="handleUpdate(row)">编辑</el-button>
          <el-button type="danger" size="mini" @click="handleDelete(row)">删除</el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination
      v-show="total>0"
      :total="total"
      :page.sync="listQuery.page"
      :limit.sync="listQuery.limit"
      @pagination="getList"
    />

    <el-dialog :title="dialogTitle" :visible.sync="dialogVisible" width="60%">
      <el-form
        ref="dataForm"
        :model="dialogForm"
        label-position="left"
        label-width="100px"
        style="width: 600px; margin-left:50px;"
      >
        <el-form-item label="类型" prop="类型">
          <el-input v-model="dialogForm.type" />
        </el-form-item>
        <el-form-item label="描述">
          <el-input
            v-model="dialogForm.typeDescription"
            :autosize="{ minRows: 2, maxRows: 4}"
            type="textarea"
            placeholder="请输入"
          />
        </el-form-item>
        <el-form-item label="默认短信内容">
          <el-input
            v-model="dialogForm.defaultSmsContent"
            :autosize="{ minRows: 2, maxRows: 4}"
            type="textarea"
            placeholder="请输入"
          />
        </el-form-item>
        <el-form-item label="默认邮件内容">
          <el-input
            v-model="dialogForm.defaultEmailContent"
            :autosize="{ minRows: 2, maxRows: 4}"
            type="textarea"
            placeholder="请输入"
          />
        </el-form-item>
        <el-form-item label="短信号码">
          <el-input
            v-model="dialogForm.smsAccounts"
            :autosize="{ minRows: 2, maxRows: 4}"
            type="textarea"
            placeholder="请输入"
          />
        </el-form-item>
        <el-form-item label="邮箱账号">
          <el-input
            v-model="dialogForm.emailAccounts"
            :autosize="{ minRows: 2, maxRows: 4}"
            type="textarea"
            placeholder="请输入"
          />
        </el-form-item>
        <el-form-item label="SQL脚本">
          <el-input
            v-model="dialogForm.sqlScript"
            :autosize="{ minRows: 2, maxRows: 4}"
            type="textarea"
            placeholder="请输入"
          />
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="dialogVisible = false">取消</el-button>
        <el-button type="primary" @click="saveData()">保存</el-button>
      </div>
    </el-dialog>
  </div>
</template>

<style>
.table-expand {
  font-size: 0;
}
.table-expand label {
  width: 120px;
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
import {
  fetchList,
  createOrUpdate,
  deleteData
} from "@/api/warningNoticeConfig";
import waves from "@/directive/waves"; // waves directive
import { parseTime } from "@/utils";
import Pagination from "@/components/Pagination"; // secondary package based on el-pagination

export default {
  name: "warningNoticeConfigList",
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
        type: undefined,
        typeDescription: undefined,
        smsAccounts: undefined,
        emailAccounts: undefined
      },
      ids: [],
      dialogVisible: false,
      dialogStatus: "",
      dialogTitle: "",
      dialogForm: {
        id: 0,
        type: undefined,
        typeDescription: undefined,
        defaultSmsContent: undefined,
        defaultEmailContent: undefined,
        smsAccounts: undefined,
        emailAccounts: undefined,
        sqlScript: undefined
      }
    };
  },
  created() {
    this.getList();
  },
  methods: {
    getList() {
      this.listLoading = true;
      fetchList(this.listQuery).then(response => {
        this.list = response.data.items;
        this.total = response.data.total;
        this.listLoading = false;
      });
    },
    handleFilter() {
      this.listQuery.page = 1;
      this.getList();
    },
    resetDialogForm() {
      this.dialogForm = {
        id: 0,
        type: undefined,
        typeDescription: undefined,
        defaultSmsContent: undefined,
        defaultEmailContent: undefined,
        smsAccounts: undefined,
        emailAccounts: undefined,
        sqlScript: undefined
      };
    },
    handleCreate() {
      this.resetDialogForm();
      this.dialogStatus = "create";
      this.dialogTitle = "新增";
      this.dialogVisible = true;
    },
    handleUpdate(row) {
      this.dialogForm = Object.assign({}, row); // copy obj
      this.dialogStatus = "update";
      this.dialogTitle = "编辑";
      this.dialogVisible = true;
    },
    saveData() {
      const data = Object.assign({}, this.dialogForm);
      createOrUpdate(data).then(response => {
        if (response.isSuccess) {
          this.dialogVisible = false;
          this.getList();
          const message =
            this.dialogStatus === "create" ? "保存成功" : "修改成功";
          this.$message({
            type: "success",
            message: message
          });
        } else {
          this.$message({
            type: "error",
            message: response.message
          });
        }
      });
    },
    handleDelete(row) {
      this.$confirm("此操作将删除该记录, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      })
        .then(() => {
          this.ids = [];
          this.ids.push(row.id);
          deleteData(this.ids).then(response => {
            if (response.isSuccess) {
              this.getList();
              this.$message({
                type: "success",
                message: "删除成功!"
              });
            } else {
              this.$message({
                type: "info",
                message: response.message
              });
            }
          });
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "已取消删除"
          });
        });
    }
  }
};
</script>
