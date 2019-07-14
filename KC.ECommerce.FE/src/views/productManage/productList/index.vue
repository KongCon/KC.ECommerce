<template>
  <div class="app-container">
    <div class="filter-container">
      <el-input
        v-model="listQuery.name"
        placeholder="请输入产品名称"
        style="width: 120px;"
        class="filter-item"
        @keyup.enter.native="handleFilter"
      />
      <el-select
        v-model="listQuery.status"
        placeholder="产品状态"
        clearable
        style="width: 120px"
        class="filter-item"
      >
        <el-option
          v-for="item in statusOptions"
          :key="item.key"
          :label="item.label"
          :value="item.key"
        />
      </el-select>
      <el-button
        v-waves
        class="filter-item"
        type="primary"
        icon="el-icon-search"
        @click="handleFilter"
      >查询</el-button>
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
      <el-table-column
        type="expand"
        label="#"
      >
        <template slot-scope="scope">
          <el-form
            label-position="left"
            inline
            class="table-expand"
          >
            <el-form-item label="短信内容">
              <span>{{ scope.row.smsContent }}</span>
            </el-form-item>
            <el-form-item label="邮件内容">
              <el-input
                type="textarea"
                :autosize="{ minRows: 2, maxRows: 5}"
                v-model="scope.row.emailContent"
              ></el-input>
            </el-form-item>
          </el-form>
        </template>
      </el-table-column>
      <el-table-column
        type="index"
        label="序号"
        width="50"
        align="center"
      ></el-table-column>
      <el-table-column
        label="ID"
        prop="id"
        align="center"
        width="80"
      >
        <template slot-scope="scope">
          <span>{{ scope.row.id }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="处理状态"
        align="center"
        width="80"
      >
        <template slot-scope="scope">
          <span>{{ scope.row.handleStatus }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="发送状态"
        align="center"
        width="80"
      >
        <template slot-scope="scope">
          <span>{{ scope.row.sendStatus }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="发送数量"
        align="center"
        width="80"
      >
        <template slot-scope="scope">
          <span>{{ scope.row.sendCount }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="发送时间"
        align="center"
        width="135"
      >
        <template slot-scope="scope">
          <span>{{ scope.row.sendTime }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="创建时间"
        align="center"
        width="135"
      >
        <template slot-scope="scope">
          <span>{{ scope.row.createTime }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="发送邮件"
        align="center"
        width="80"
      >
        <template slot-scope="scope">
          <span>{{ scope.row.isSendEmail }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="发送短信"
        align="center"
        width="80"
      >
        <template slot-scope="scope">
          <span>{{ scope.row.isSendSms }}</span>
        </template>
      </el-table-column>
      <el-table-column
        label="配置类型"
        align="center"
        show-overflow-tooltip
      >
        <template slot-scope="scope">
          <span>{{ scope.row.configType }}</span>
        </template>
      </el-table-column>

      <el-table-column
        fixed="right"
        label="操作"
        align="center"
        width="120"
        class-name="small-padding fixed-width"
      >
        <template slot-scope="{row}">
          <el-button
            type="primary"
            v-if="row.handleStatus==='未处理'"
            size="mini"
            @click="openHandleDialog(row)"
          >处理</el-button>
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

    <el-dialog
      title="处理状态更改"
      :visible.sync="dialogUpdateVisible"
    >
      <el-form
        :model="updateParas"
        label-width="70px"
        style="width: 500px; margin-left:50px;"
      >
        <el-form-item
          label="短信内容"
          v-if="show.sms"
        >
          <!-- <el-input v-model="updateParas.smsContent" autocomplete="off" maxlength="70"></el-input> -->
          <el-input
            type="text"
            placeholder="请输入内容"
            v-model="updateParas.smsContent"
            maxlength="70"
            show-word-limit
          ></el-input>
        </el-form-item>
        <el-form-item
          label="邮件内容"
          v-if="show.email"
        >
          <!-- <el-input v-model="updateParas.emailContent" autocomplete="off"></el-input> -->
          <!-- <el-input type="textarea" :autosize="{ minRows: 2, maxRows: 5}" v-model="updateParas.emailContent"></el-input> -->
          <el-input
            type="textarea"
            :autosize="{ minRows: 2, maxRows: 5}"
            placeholder="请输入内容"
            v-model="updateParas.emailContent"
            maxlength="500"
            show-word-limit
          ></el-input>
        </el-form-item>
      </el-form>
      <div
        slot="footer"
        class="dialog-footer"
      >
        <el-button @click="dialogUpdateVisible = false">取 消</el-button>
        <el-button
          type="primary"
          @click="updateHandleStatus()"
        >确 定</el-button>
      </div>
    </el-dialog>
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
import { fetchList, changeHandleStatus } from "@/api/warningNotice";
import waves from "@/directive/waves"; // waves directive
import { parseTime } from "@/utils";
import Pagination from "@/components/Pagination"; // secondary package based on el-pagination

export default {
  name: "warningNoticeList",
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
        id: undefined,
        handleStatus: 0,
        smsContent: undefined,
        emailContent: undefined
      },
      handleOptions: [
        { label: "全部", key: 0 },
        { label: "未处理", key: 1 },
        { label: "已处理", key: 2 }
      ],
      dialogUpdateVisible: false,
      updateParas: {
        ids: [],
        smsContent: undefined,
        emailContent: undefined
      },
      show: {
        sms: false,
        email: false
      }
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
      fetchList(this.listQuery).then(response => {
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
    },
    openHandleDialog(row) {
      this.updateParas.ids = [];
      this.updateParas.ids.push(row.id);
      this.updateParas.smsContent = "";
      this.updateParas.emailContent = "";
      this.show.sms = row.isSendSms === "是";
      this.show.email = row.isSendEmail === "是";
      this.dialogUpdateVisible = true;
    },
    updateHandleStatus() {
      changeHandleStatus(this.updateParas).then(response => {
        if (response.isSuccess) {
          this.dialogUpdateVisible = false;
          this.getList();
          this.$message({
            type: "success",
            message: "处理成功!"
          });
        } else {
          this.$message({
            type: "error",
            message: response.message
          });
        }
      });
    }
  }
};
</script>
