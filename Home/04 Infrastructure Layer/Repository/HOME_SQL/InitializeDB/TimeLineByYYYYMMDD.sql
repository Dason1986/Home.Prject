create view TimeLineByYYYYMMDD
as
 
 


 
 SELECT       SUBSTRING (AttValue ,0,11) TimeLine,   COUNT(0)   'Count'
                 FROM      PhotoAttribute
                 WHERE   (AttKey = 'DateTimeDigitized' and AttValue !='unknown') 
Union 
select 'unknown'   TimeLine, count(0)   'Count'
                 FROM      PhotoAttribute
                 WHERE   (AttKey = 'DateTimeDigitized' and AttValue='unknown')