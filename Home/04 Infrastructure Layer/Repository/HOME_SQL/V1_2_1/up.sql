﻿INSERT INTO [serialnumbermanagement] ([ID],[Code],[CurrentNumber],[SerialNumberFormat],[IsCustom],[CustomClass],[StatusCode],[Created],[CreatedBy]) VALUES (N'4aae2b32-f4f6-e511-9845-acbc32c58e76',N'File',0,N'{Number:d12}',0,NULL,2,N'2017-09-13 11:54:07',N'CreateScript');
INSERT INTO  [ScheduleJob] ([Id], [Title], [ScheduleJobClass],   [ScheduleCronExpression],   [Created], [CreatedBy], [Modified], [ModifiedBy], [StatusCode]) VALUES (N'2260fb81-18f6-4785-b01d-b046fe4ae2de', N'刪除重複文件', N'HomeApplication.Jobs.DeleteFileDistinctByMD5,HomeApplication',   N'0 0/5 * * * ?',  now(), N'CreateScript', now(), N'CreateScript',2);