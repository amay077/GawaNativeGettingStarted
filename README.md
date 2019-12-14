Xamarin.Forms でガワネイティブアプリを作るときのテンプレートプロジェクトを作る１

今年は専ら Angular で Webアプリを作ったり、ガワネイティブアプリを作ったりしています。

## 概要

最近は、モバイルネイティブアプリよりも SPA/PWA、そしてそれを利用したガワネイティブアプリを推奨している私ですが、ガワネイティブアプリを作る時の「ガワ」には Xamarin(Xamarin.Forms) を採用しています。

ガワネイティブアプリは、Webアプリがネイティブの機能を欲するから採用されるわけで、それを制御するためにWebアプリとネイティブ機能の相互通信が必要になります。

また、「ガワ」は ``WebView`` なわけですが、それがアプリとして自然に振る舞うために、いくつかの「設定」をしてあげる必要があります。

この記事は、そのような「Xamarin(.Forms) でガワネイティブアプリを作るときのリファレンス」になればよいなと思って書きます。

尚、~~Advent Calendar の締め切りに間に合わせるために~~ 意外と情報量が多かったので、前編と後編に分けます。
今回は前編です。

## 目次

1. 日本語入力時の画面高さの調整
2. ステータスバー、あるいは SafeArea(ノッチ部)の色
3. 【次回以降予定】アプリ情報の Web 側への引き渡し
4. 【次回以降予定】``<input type="xxx">`` への対応
5. 【次回以降予定】Back ボタンハンドリングの Web 側への移譲

## 1. 日本語入力時の画面高さの調整

ソフトウェアキーボードが、コンテンツの手前が重なってしまう問題の解決です。

これは、

* [Xamarin.Forms でソフトウェアキーボードが表示された時に画面が隠れないようにする - Qiita](https://qiita.com/amay077/items/6fcdec829a96bc604532)

を適用して解決します。

### Android の場合

今回のように WebView だけ対応すればよい場合は、 ``MainActivity.cs`` に ```.UseWindowSoftInputModeAdjust()`` の行を追加してあげればよいみたいです。

```csharp
// MainActivity.cs
protected override void OnCreate(Bundle bundle)
{
    TabLayoutResource = Resource.Layout.Tabbar;
    ToolbarResource = Resource.Layout.Toolbar;

    base.OnCreate(bundle);
    global::Xamarin.Forms.Forms.Init(this, bundle);
    LoadApplication(new App());

	Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>()
		.UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize); // ←ここ！！
}
```

### iOS の場合

iOS の場合、前出のわたしのエントリをリライトして頂いた、

* [Xamarin.Formsでソフトウェアキーボードで入力欄が隠れないようにする2018 - Qiita](https://qiita.com/sun_bacon/items/0cafaeaf34abe9f86e95#_reference-cb351a7b1ca951eb8084)

があるのですが、WebView だけ対応すればよい場合、特にやることは無いですｗ

## 参考

* [iOSでWebViewを開発する際に気をつけるべき9のこと - Qiita](https://qiita.com/nikadon/items/1a38761012d530db112a)
* [(Xamarin)Webサイトのオフライン閲覧アプリを作るときの注意 - Qiita](https://qiita.com/khosokawa/items/e319c5ca65e3de9c81db)
* [WebViewの<input type="file">でカメラ、画像からデータを取得する [Android]](https://www.petitmonte.com/java/android_webview_camera.html)
* [html5 カメラ起動 capture属性について - Qiita](https://qiita.com/RIKIgigasu/items/0c710fa608dd5a7a45cb)
* [Materialize](https://materializecss.com/)
* [SafeAreaInsets For Xamarin.Forms In iOS - Xamarin Help](https://xamarinhelp.com/safeareainsets-xamarin-forms-ios/)
* [ステータスバーのスタイル(色)変更方法優先度 まとめ - Qiita](https://qiita.com/ux_design_tokyo/items/8e62977b7609e68755c7)