# RoosterLottery

## Hướng dẫn cài đặt Microsoft SQL Server

Mở Terminal và dùng [winget](https://learn.microsoft.com/en-us/windows/package-manager/winget/) để cài đặt Microsoft SQL Server bằng cách gõ command:

```shell
> winget install Microsoft.SQLServer.2022.Developer
```

(Optional) Cài đặt Microsoft SQL Server Management Studio dùng để phát triển stored procedures.

```shell
> winget install Microsoft.SQLServerManagementStudio
```

Người phát triển có thể backup database và push file .bak lên repository hoặc sử dụng một migration framework tùy chọn.
