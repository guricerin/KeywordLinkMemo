# KeywordLinkMemo

自動キーワードリンク機能付きのメモ用アプリ。  

![demo](https://user-images.githubusercontent.com/38801778/153973667-545f7028-9ff4-4ea6-9675-3f93637450dc.gif)

## 依存

- .NET Framework v4.7.2  

## 使い方

### 実行

- 配布zipファイルを解凍すると``KeywordLinkMemo``というフォルダができます。
- その中の``KeywordLinkMemo.exe``をダブルクリックすることで本アプリは作動します。

### 仕様

- 自動キーワードリンクは``グループ``単位で行われます。
- ``グループ``の実態は、exeファイルと同階層のフォルダに作成される``memos``フォルダ内のフォルダです。
- ``メモ項目``の実態は、グループフォルダ内に作成されるtxtファイルです。
- 本アプリのバージョンアップの際は、``memos``フォルダをコピペすることで移行可能です。

### アンインストール

- ``KeywordLinkMemo``フォルダを削除するだけでアンインストール可能です。
