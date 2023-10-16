import myaxios from '@/utils/myaxios'

export default{
   upload(data:any){
    return myaxios({
        url: `/file`,
        headers:{"Content-Type": "application/x-www-form-urlencoded; charset=UTF-8"},
        method: 'post',
        data:data
      });
} 
}