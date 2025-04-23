# pleasanter-api-tester
2025/04/24 記

## 概要
「[OSSのノーコード・ローコード開発ツール　Pleasanter](https://pleasanter.org/)」（以下「プリザンター」）のAPIを呼び出すツールです。  
作者がプリザンターに関わる作業を行う際に使用しているものです。

### 対応API
- [レコード一括削除 bulkdelete](https://pleasanter.org/ja/manual/api-table-bulk-delete)
- 後述の「拡張方法」の通りに拡張することで他のAPIを呼び出すことが可能です。

## 特徴
C#のソースコードであるため、その場でコードを改変し、たとえば「繰り返し実行」の制御を書き加え、1000回連続でAPIを呼び出す等の柔軟な使い方ができます。

## 動作確認環境
1. Windows11
1. .NET9をインストール済み
1. VS Codeのターミナル（Ponwershell）と、VS Codeのデバッグコンソールで起動を確認済み
1. VS Codeには拡張機能の「C#」「C# Dev Kit」をインストール済み
1. プリザンターのバージョン：1.4.15.0
    - APIの仕様が一致すれば他のバージョンでも問題ありません。詳細は[プリザンターのマニュアル](https://pleasanter.org/ja/manual)を参照。

## 操作手順
1. 事前準備①  
    プリザンターはあらかじめセットアップする必要があります。  
1. 事前準備②  
    Program.csの定数ApiUrlとApiKeyの`""`部分に内容を記載します。  
    - 現在のソースコードの内容のまま利用できるApiUrlは、[bulkdelete](https://pleasanter.org/ja/manual/api-table-bulk-delete)です。
1. 事前準備③  
    bulkdeleteの使用時は、{WHAT'S TEST?} を`Selected = TEST_1_1_SELECTED`等に書き換えるとそのまま使用できます。  
    bulkdelete以外を呼び出す場合は、ソースコードの必要な箇所をさらに書き換えます。詳細は下記「拡張方法」を参照。
1. 起動  
    以下コマンドを実行します。
    ```
    dotnet build
    dotnet run
    ```
1. 実行後メッセージを確認  
    APIの実行に成功するとコンソールにレスポンスのメッセージが表示されます。
    ```
    2 件 削除しました。
    ```

## 拡張方法
1. PleasanterApiFieldesに任意のフィールドを追加します。その際にJsonPropertyも併せて設定します。
1. PleasanterApiFieldesのさらに配下の別クラスやそのフィールドも追加します。プリザンターのマニュアルに書かれているAIPの仕様と一致するように定義します。
1. PostAsyncメソッドのjsonContent = newのcontentを指定します。PleasanterApiFieldesクラスのインスタンスを指定します。
   - 既にある`private static readonly List<string> TEST_1_1_SELECTED`のように、外に変数を設けて詳細な設定はそちらに移管すると再利用がしやすくなります。

## 関連情報
- [プリザンター｜OSSのノーコード・ローコード開発ツール](https://pleasanter.org/)
