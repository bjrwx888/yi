#!/bin/bash
kill $(ps -aux | grep Yi.Furion.Web.Entry.dll | awk '{print $2}')
echo "Yi-进程已关闭"
