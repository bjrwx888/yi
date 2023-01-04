<template>
  <div class="app-container">
    <el-form :model="queryParams" ref="queryRef" :inline="true" v-show="showSearch" label-width="90px">
      <el-form-item label="供应商名称" prop="name">
        <el-input v-model="queryParams.name" placeholder="请输入供应商名称" clearable style="width: 240px"
          @keyup.enter="handleQuery" prop="name" />
      </el-form-item>
      <el-form-item label="采购单编号" prop="code">
        <el-input v-model="queryParams.code" placeholder="请输入采购单编号" clearable style="width: 240px"
          @keyup.enter="handleQuery" prop="code" />
      </el-form-item>

      <el-form-item label="采购员" prop="buyer">
        <el-input v-model="queryParams.code" placeholder="请输入采购员" clearable style="width: 240px"
          @keyup.enter="handleQuery" prop="buyer" />
      </el-form-item>
      <!-- <el-form-item label="状态" prop="isDeleted">
            <el-select
              v-model="queryParams.isDeleted"
              placeholder="状态"
              clearable
              style="width: 240px"
            >
              <el-option
                v-for="dict in sys_normal_disable"
                :key="dict.value"
                :label="dict.label"
                :value="dict.value"
              />
            </el-select>
          </el-form-item> -->
      <!-- <el-form-item label="创建时间" style="width: 308px">
            <el-date-picker
              v-model="dateRange"
              value-format="YYYY-MM-DD"
              type="daterange"
              range-separator="-"
              start-placeholder="开始日期"
              end-placeholder="结束日期"
            ></el-date-picker>
          </el-form-item> -->
      <el-form-item>
        <el-button type="primary" icon="Search" @click="handleQuery">搜索</el-button>
        <el-button icon="Refresh" @click="resetQuery">重置</el-button>
      </el-form-item>
    </el-form>

    <el-row :gutter="10" class="mb8">
      <el-col :span="1.5">
        <el-button type="primary" plain icon="Plus" @click="handleAdd" v-hasPermi="['erp:purchase:add']">新增</el-button>
      </el-col>
      <el-col :span="1.5">
        <el-button type="success" plain icon="Edit" :disabled="single" @click="handleUpdate"
          v-hasPermi="['erp:purchase:edit']">修改</el-button>
      </el-col>
      <el-col :span="1.5">
        <el-button type="danger" plain icon="Delete" :disabled="multiple" @click="handleDelete"
          v-hasPermi="['erp:purchase:remove']">删除</el-button>
      </el-col>
      <el-col :span="1.5">
        <el-button type="warning" plain icon="Download" @click="handleExport"
          v-hasPermi="['erp:purchase:export']">导出</el-button>
      </el-col>
      <right-toolbar v-model:showSearch="showSearch" @queryTable="getList"></right-toolbar>
    </el-row>

    <el-table v-loading="loading" :data="dataList" @selection-change="handleSelectionChange">
      <el-table-column type="selection" width="55" align="center" />

      <!-----------------------这里开始就是数据表单的全部列------------------------>
      <el-table-column label="采购单号" align="center" prop="code" />

      <el-table-column label="供应商" align="center" prop="name" :show-overflow-tooltip="true" />

      <el-table-column label="需求时间" align="center" prop="needTime" :show-overflow-tooltip="true" />

      <el-table-column label="采购员" align="center" prop="buyer" :show-overflow-tooltip="true" />

      <el-table-column label="总共金额" align="center" prop="totalMoney" :show-overflow-tooltip="true" />

      <el-table-column label="已支付金额" align="center" prop="paidMoney" :show-overflow-tooltip="true" />

      <el-table-column label="采购状态" align="center" prop="purchaseState" :show-overflow-tooltip="true" />

      <el-table-column label="备注" align="center" prop="remarks" :show-overflow-tooltip="true" />
      <!-- <el-table-column label="状态" align="center" prop="isDeleted">
            <template #default="scope">
              <dict-tag
                :options="sys_normal_disable"
                :value="scope.row.isDeleted"
              />
            </template>
          </el-table-column> -->
      <!-- <el-table-column
            label="备注"
            align="center"
            prop="remark"
            :show-overflow-tooltip="true"
          />
          <el-table-column
            label="创建时间"
            align="center"
            prop="createTime"
            width="180"
          >
            <template #default="scope">
              <span>{{ parseTime(scope.row.createTime) }}</span>
            </template>
          </el-table-column> -->
      <el-table-column label="操作" align="center" class-name="small-padding fixed-width">
        <template #default="scope">
          <el-button type="text" icon="Edit" @click="handleUpdate(scope.row)"
            v-hasPermi="['business:article:edit']">查看</el-button>

          <el-button type="text" icon="Edit" @click="handleUpdate(scope.row)"
            v-hasPermi="['business:article:edit']">修改</el-button>

          <el-button type="text" icon="Delete" @click="handleDelete(scope.row)"
            v-hasPermi="['business:article:remove']">结束</el-button>
          <el-button type="text" icon="Delete" @click="handleDelete(scope.row)"
            v-hasPermi="['business:article:remove']">删除</el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination v-show="total > 0" :total="total" v-model:page="queryParams.pageNum"
      v-model:limit="queryParams.pageSize" @pagination="getList" />

    <!-- ---------------------这里是新增和更新的对话框--------------------- -->
    <el-dialog :title="title" v-model="open" width="1200px" append-to-body>
      <el-form ref="dataRef" :model="form" :rules="rules" label-width="100px">
        <el-row :gutter="20">
          <el-col :span="8">
            <el-form-item label="采购单编码" prop="code">
              <el-input v-model="form.code" placeholder="请输入采购单编码" />
            </el-form-item>
          </el-col>

          <el-col :offset="8" :span="8">
            <el-form-item label="采购单员" prop="name">
              <el-input v-model="form.name" placeholder="请输入采购单员" /> </el-form-item>
          </el-col>

          <el-col :span="8">
            <el-form-item label="需求时间" prop="name">
              <el-input v-model="form.name" placeholder="请输入采购单员" />
            </el-form-item>
          </el-col>

          <el-col :offset="8" :span="8">
            <el-form-item label="总金额" prop="name">
              999{{ form.name }}
            </el-form-item>
          </el-col>


        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item label="物料信息">
              <el-table :data="tableData" border style="width: 100%">

                <el-table-column  width="90">
                  <template #default>
                    <el-button   icon="Delete" type="danger" size="small">删除</el-button>
                  </template>
                </el-table-column>

                <el-table-column prop="date" label="物料" width="180" />
                <el-table-column prop="name" label="单价" width="180" />
                <el-table-column prop="address" label="采购数量" width="180" />
                <el-table-column prop="address" label="备注" />
              </el-table>
            </el-form-item>
          </el-col>
        </el-row>
        <el-form-item label="备注" prop="remarks">
          <el-input v-model="form.remarks" type="textarea" placeholder="请输入内容" :rows="5"></el-input>
        </el-form-item>
      </el-form>
      <template #footer>
        <div class="dialog-footer">
          <el-button type="primary" @click="submitForm">确 定</el-button>
          <el-button @click="cancel">取 消</el-button>
        </div>
      </template>
    </el-dialog>
  </div>
</template>
    
<script setup>
import {
  listData,
  getData,
  delData,
  addData,
  updateData,
} from "@/api/erp/purchaseApi";
import { ref } from "@vue/reactivity";

const { proxy } = getCurrentInstance();
const { sys_normal_disable } = proxy.useDict("sys_normal_disable");

const tableData = [
  {
    date: '2016-05-03',
    name: 'Tom',
    address: 'No. 189, Grove St, Los Angeles',
  },
  {
    date: '2016-05-02',
    name: 'Tom',
    address: 'No. 189, Grove St, Los Angeles',
  },
  {
    date: '2016-05-04',
    name: 'Tom',
    address: 'No. 189, Grove St, Los Angeles',
  },
  {
    date: '2016-05-01',
    name: 'Tom',
    address: 'No. 189, Grove St, Los Angeles',
  },
]


const dataList = ref([]);
const open = ref(false);
const loading = ref(true);
const showSearch = ref(true);
const ids = ref([]);
const single = ref(true);
const multiple = ref(true);
const total = ref(0);
const title = ref("");
const dateRange = ref([]);
const data = reactive({
  form: {},
  queryParams: {
    pageNum: 1,
    pageSize: 10,
    name: undefined,
    code: undefined,
    buyer: undefined,
  },
  rules: {
    code: [{ required: true, message: "采购单编号不能为空", trigger: "blur" }],
    name: [{ required: true, message: "采购单名称不能为空", trigger: "blur" }],
  },
});

const { queryParams, form, rules } = toRefs(data);

/** 查询列表 */
function getList() {
  loading.value = true;
  listData(proxy.addDateRange(queryParams.value, dateRange.value)).then(
    (response) => {
      dataList.value = response.data.data;
      total.value = response.data.total;
      loading.value = false;
    }
  );
}
/** 取消按钮 */
function cancel() {
  open.value = false;
  reset();
}

/** 表单重置 */
function reset() {
  proxy.resetForm("dataRef");
}
/** 搜索按钮操作 */
function handleQuery() {
  queryParams.value.pageNum = 1;
  getList();
}
/** 重置按钮操作 */
function resetQuery() {
  dateRange.value = [];
  proxy.resetForm("queryRef");
  handleQuery();
}
/** 新增按钮操作 */
function handleAdd() {
  reset();
  open.value = true;
  title.value = "添加采购单";
}
/** 多选框选中数据 */
function handleSelectionChange(selection) {
  ids.value = selection.map((item) => item.id);
  single.value = selection.length != 1;
  multiple.value = !selection.length;
}
/** 修改按钮操作 */
function handleUpdate(row) {
  reset();
  const id = row.id || ids.value;
  getData(id).then((response) => {
    form.value = response.data;
    open.value = true;
    title.value = "修改采购单";
  });
}
/** 提交按钮 */
function submitForm() {
  proxy.$refs["dataRef"].validate((valid) => {
    if (valid) {
      if (form.value.id != undefined) {
        updateData(form.value.id, form.value).then((response) => {
          proxy.$modal.msgSuccess("修改成功");
          open.value = false;
          getList();
        });
      } else {
        addData(form.value).then((response) => {
          proxy.$modal.msgSuccess("新增成功");
          open.value = false;
          getList();
        });
      }
    }
  });
}
/** 删除按钮操作 */
function handleDelete(row) {
  const delIds = row.id || ids.value;
  proxy.$modal
    .confirm('是否确认删除编号为"' + delIds + '"的数据项？')
    .then(function () {
      return delData(delIds);
    })
    .then(() => {
      getList();
      proxy.$modal.msgSuccess("删除成功");
    })
    .catch(() => { });
}
/** 导出按钮操作 */
function handleExport() { }

getList();
</script>