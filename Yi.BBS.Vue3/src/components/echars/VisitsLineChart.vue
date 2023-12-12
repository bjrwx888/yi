<template>
  <div ref="VisitsLineChart"></div>
</template>
<script setup>
import * as echarts from "echarts/core";
import { GridComponent } from "echarts/components";
import { LineChart } from "echarts/charts";
import { UniversalTransition } from "echarts/features";
import { CanvasRenderer } from "echarts/renderers";
import { ref, onMounted } from "vue";
import { getWeek } from "@/apis/accessApi.js";
echarts.use([GridComponent, LineChart, CanvasRenderer, UniversalTransition]);

const VisitsLineChart = ref(null);

onMounted(async () => {
  var myChart = echarts.init(VisitsLineChart.value, null, {
    width: 320,
    height: 230,
  });
  var option;

  const response = await getWeek();

  var numberData = response.data.map((x) => x.number);

  option = {
    xAxis: {
      type: "category",
      boundaryGap: false,
      data: ["周一", "周二", "周三", "周四", "周五", "周六", "周日"],
    },
    yAxis: {
      type: "value",
    },
    series: [
      {
        data: numberData,
        type: "line",
        areaStyle: {},
      },
    ],
  };

  option && myChart.setOption(option);

  window.addEventListener("resize", function () {
    myChart.resize();
  });
});
</script>
