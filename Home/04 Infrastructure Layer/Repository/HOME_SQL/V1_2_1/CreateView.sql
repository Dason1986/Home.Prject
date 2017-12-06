create  view DuplicateByMD5View
as 
select  md5 ,count(0) 'count'  from  fileinfo where StatusCode = 2 group  by  md5  having  count(md5) > 1  
   