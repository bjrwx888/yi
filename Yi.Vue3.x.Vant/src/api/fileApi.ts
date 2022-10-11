import myaxios from '@/utils/myaxios.ts'

export default{
   upload(type:string,data:any){
    return myaxios({
        url: `/upload/${type}`,
        headers:{"Content-Type": "application/x-www-form-urlencoded; charset=UTF-8"},
        method: 'POST',
        data:data
      });
} 
}