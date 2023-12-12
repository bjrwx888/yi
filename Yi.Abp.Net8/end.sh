#!/bin/bash
kill $(ps -aux | grep Yi.Abp.Web.dll | awk '{print $2}')
echo "Yi-进程已关闭"
