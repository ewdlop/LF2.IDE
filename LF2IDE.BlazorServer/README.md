# LF2 IDE - Blazor Server

🎮 **Little Fighter 2 整合開發環境** - 使用 Blazor Server 重新打造的現代化網頁版本

## 📋 專案簡介

這是將原本的 WinForms LF2 IDE 轉換為 Blazor Server 應用程式的版本，保留了原有的核心功能，並提供更現代化的網頁界面體驗。

## ✨ 核心功能

### 已實現
- ✅ **代碼編輯器** - 整合 Monaco Editor，支援語法高亮
- ✅ **檔案瀏覽器** - 瀏覽和打開 LF2 資料檔案
- ✅ **LF2 數據加密/解密** - 自動處理 .dat 檔案加密
- ✅ **框架列表** - 顯示所有角色動畫框架
- ✅ **屬性面板** - 顯示檔案基本資訊
- ✅ **多檔案編輯** - 支援同時開啟多個檔案

### 待實現
- ⏳ 視覺化設計器（Bdy/Itr 編輯）
- ⏳ 精靈查看器
- ⏳ 即時數據載入器（IDL）
- ⏳ 插件系統

## 🏗️ 架構設計

```
LF2IDE.BlazorServer/
├── Components/
│   ├── Layout/
│   │   ├── MainLayout.razor          # 主佈局
│   │   └── MainLayout.razor.css      # 佈局樣式
│   ├── Pages/
│   │   └── Home.razor                # 主頁/編輯器頁面
│   ├── CodeEditor.razor              # Monaco 編輯器組件
│   ├── FileExplorer.razor            # 檔案瀏覽器組件
│   └── FrameList.razor               # 框架列表組件
├── Models/
│   ├── DataType.cs                   # 數據類型枚舉
│   ├── ObjectType.cs                 # 物件類型枚舉
│   ├── Frame.cs                      # 框架數據模型
│   ├── SpriteSheet.cs                # 精靈圖表模型
│   ├── LF2DataFile.cs                # LF2 數據檔案模型
│   └── ProjectFile.cs                # 專案檔案模型
├── Services/
│   ├── IFileService.cs               # 檔案服務介面
│   ├── FileService.cs                # 檔案服務實作
│   ├── ILF2DataService.cs            # LF2 數據服務介面
│   ├── LF2DataService.cs             # LF2 數據服務實作（加密/解密/解析）
│   └── EditorStateService.cs         # 編輯器狀態管理
└── wwwroot/
    └── app.css                       # 全域樣式
```

## 🎯 核心技術

- **.NET 8.0** - 最新的 .NET 框架
- **Blazor Server** - 伺服器端 Blazor
- **BlazorMonaco** - Monaco Editor for Blazor
- **SignalR** - 即時通訊
- **Razor Components** - UI 組件系統

## 🚀 使用方式

### 環境需求

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) 或更高版本
- 支援的瀏覽器（Chrome, Edge, Firefox, Safari）

### 安裝依賴

```bash
cd LF2IDE.BlazorServer
dotnet restore
```

### 執行應用程式

```bash
dotnet run
```

然後開啟瀏覽器訪問：
- HTTP: http://localhost:5000
- HTTPS: https://localhost:5001

### 建置發佈版本

```bash
dotnet publish -c Release
```

## 📖 使用說明

### 1. 開啟 LF2 專案

1. 在左側檔案瀏覽器輸入您的 LF2 目錄路徑（例如：`C:\LF2`）
2. 點擊刷新按鈕 🔄
3. 瀏覽並點擊 `.dat` 檔案進行編輯

### 2. 編輯數據檔案

- 點擊檔案後，內容將在 Monaco 編輯器中顯示
- 如果檔案是加密的，會自動解密
- 編輯完成後，可以儲存變更

### 3. 查看框架資訊

- 切換到「框架列表」標籤
- 查看所有動畫框架的索引和屬性
- 點擊框架可查看詳細資訊

### 4. 屬性面板

右側屬性面板顯示：
- 檔案名稱
- 加密狀態
- 框架數量
- 精靈數量

## 🔧 配置說明

### LF2 數據加密

預設加密密鑰：`odBearBecauseHeIsVeryGoodSiuHungIsAGo`

如需修改，請編輯 `Services/LF2DataService.cs` 中的 `DefaultPassword` 常數。

### 檔案路徑

預設 LF2 路徑設定在 `Components/FileExplorer.razor` 中：
```csharp
private string currentPath = @"C:\LF2";
```

## 🎨 界面特色

- **現代化設計** - 美觀的漸層色彩和卡片式佈局
- **響應式佈局** - 三欄式佈局（檔案瀏覽器、編輯器、屬性面板）
- **多檔案標籤** - 類似 VS Code 的標籤式檔案管理
- **即時更新** - 使用 SignalR 實現即時狀態同步

## 🧩 擴展功能

### 自訂編輯器主題

修改 `CodeEditor.razor` 中的 `EditorConstructionOptions`：

```csharp
Theme = "vs-dark", // 可選: "vs", "vs-dark", "hc-black"
```

### 添加新的檔案類型支援

在 `FileExplorer.razor` 的 `GetFileIcon` 方法中添加：

```csharp
".yourext" => "📋",
```

## 🐛 已知問題

1. 目前僅支援基本的文字編輯，視覺化設計器尚未實現
2. IDL（即時數據載入器）功能需要額外的原生互操作
3. 檔案儲存功能需要完善錯誤處理

## 📝 待辦事項

- [ ] 實現視覺化 Bdy/Itr 編輯器
- [ ] 添加精靈圖片查看器
- [ ] 實現檔案儲存功能
- [ ] 添加快捷鍵支援
- [ ] 實現多語言支援
- [ ] 添加主題切換功能
- [ ] 實現搜尋和取代功能
- [ ] 添加版本控制整合

## 🤝 貢獻

歡迎提交 Issue 和 Pull Request！

## 📄 授權

本專案採用 MIT 授權。

---

**原始專案**: [LF2 IDE (WinForms)](https://github.com/ahmetsait/LF2.IDE)  
**技術支援**: 基於 .NET 8.0 和 Blazor Server

