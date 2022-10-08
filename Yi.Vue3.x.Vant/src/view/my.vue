<template>
    <van-row class="headRow">
        <van-col span="2"><van-icon name="scan" size="1.5rem"/></van-col>
        <van-col span="20"></van-col>
        <van-col span="2"><van-icon name="setting-o" size="1.5rem"/></van-col>
    </van-row>   

    <van-row class="bodyRow">
        <van-col span="6" class="leftCol">
          <van-image
            round
            width="4rem"
            height="4rem"
            src="https://fastly.jsdelivr.net/npm/@vant/assets/cat.jpeg"
          />
          </van-col>
          <van-col span="12"><span class="title">大白不在家</span></van-col>
          <van-col span="6"><span class="subtitle">个人主页<van-icon name="arrow" /></span></van-col>


          <van-col span="6" class="bodyCol"><div><span>6</span><br>关注</div></van-col>
          <van-col span="6" class="bodyCol"><div><span>3</span><br>粉丝</div></van-col>
          <van-col span="6" class="bodyCol"><div><span>0</span><br>人气</div></van-col>
          <van-col span="6" class="bodyCol"><div><span>3</span><br>钱钱</div></van-col>

          <van-col span="24">
        <van-row class="btnRow">
            <van-col span="12">     <van-button class="btn" type="primary" >我的购物</van-button></van-col>
            <van-col span="12" >   <van-button class="btn" type="primary">我的签到</van-button></van-col>
        </van-row>
          </van-col >
    </van-row>
</template>
<style scoped>
.headRow{
    padding: 0.5rem 0.5rem 0.5rem 0.5rem;
}
.bodyRow
{
    text-align: left;
    margin-top: 2rem;
    padding-left: 1.5rem;
    padding-right: 1.5rem;;
}
.bodyRow span{
    line-height: 2rem;

}
.title
{
    font-size: 1.5rem;
}
.subtitle
{
    color:#CBCBCB ;
}
.bodyCol{
    text-align: center;
    font-size: 1.2rem;
    margin-top: 1rem;
}
.btn{
    width: 100%;
    margin: 0;
    background-color: #FFFFFF;
    border:none;
    color: black;
}
.btnRow{
    margin-top: 1.5rem;
    box-shadow: 0rem 0rem 0.2rem 0.2rem #f3f3f3;
}
</style>