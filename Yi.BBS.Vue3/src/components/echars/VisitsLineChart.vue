<template>
    <div ref="VisitsLineChart"></div>
</template>
<script setup>
import * as echarts from 'echarts/core';
import { GridComponent } from 'echarts/components';
import { LineChart } from 'echarts/charts';
import { UniversalTransition } from 'echarts/features';
import { CanvasRenderer } from 'echarts/renderers';
import { ref ,onMounted} from 'vue';

echarts.use([GridComponent, LineChart, CanvasRenderer, UniversalTransition]);

const VisitsLineChart=ref(null);

onMounted(()=>{
  var myChart = echarts.init(VisitsLineChart.value, null, {
    width: 320,
    height: 230
  });
var option;

option = {
  xAxis: {
    type: 'category',
    boundaryGap: false,
    data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun']
  },
  yAxis: {
    type: 'value'
  },
  series: [
    {
      data: [82, 93, 90, 93, 129, 133, 132],
      type: 'line',
      areaStyle: {}
    }
  ]
};

option && myChart.setOption(option);

window.addEventListener('resize', function() {
    myChart.resize();
  });
})




</script>