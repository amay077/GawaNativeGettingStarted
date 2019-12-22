Xamarin.Forms でガワネイティブアプリを作るときのテンプレートプロジェクトを作る２

## 目次

1. 【第1回】日本語入力時の画面高さの調整
2. 【第1回】ステータスバー、あるいは SafeArea(ノッチ部)の色
3. 【次回以降予定】アプリ情報の Web 側への引き渡し
4. 【次回以降予定】``<input type="xxx">`` への対応
5. 【次回以降予定】Back ボタンハンドリングの Web 側への移譲
6. スプラッシュスクリーンおよび初回読み込み時の対応

## 6. スプラッシュスクリーンおよび初回読み込み時の対応

実体がWebアプリであるため、起動してからアプリが使用可能になるまでに時間がかかるのは、ガワネイティブの弱点の一つです。
これはなんとかごまかしてユーザーに不快感を与えないようにしたいです。

ガワネイティブの起動にかかるプロセスは大きくわけて２つです。

1. ネイティブアプリとしての起動から最初の画面が表示されるまで
2. 最初の画面が表示されてから Web ページの読み込みが完了するまで

1 は通常のアプリでも必要なプロセス、2 はガワネイティブ特有の要件です。

まずは 1 を対応します。

### Android の場合

* [Splash Screen - Xamarin | Microsoft Docs](https://docs.microsoft.com/en-us/xamarin/android/user-interface/splash-screen)

こちらを参考にします。

Android 側プロジェクトの ``Resources/drawable/splash.xml`` を追加して、次のように記述します。

**splash.axml**

```xml

```

次に、``Resources/values/colors.xml`` の ``colorPrimaryDark`` の色を Web アプリのテーマ色に合わせます。
この例では ``#66BB6A`` とします。

```xml
<?xml version="1.0" encoding="utf-8"?>
<resources>
    <color name="launcher_background">#FFFFFF</color>
    <color name="colorPrimary">#3F51B5</color>
    <color name="colorPrimaryDark">#66BB6A</color>
    <color name="colorAccent">#FF4081</color>
</resources>
```

前述のリンクでは、スプラッシュスクリーン用の Theme と Activity を作るよう書かれていますが、ガワネイティブの場合少しサボることができます。
``Resources/values/styles.xml`` を開き、``MainTheme.Base`` に ``android:windowBackground`` を追加します。

**styles.xml**

```xml
<?xml version="1.0" encoding="utf-8"?>
<resources>
    <style name="MainTheme" parent="MainTheme.Base">
    </style>
    <!-- Base theme applied no matter what API -->
    <style name="MainTheme.Base" parent="Theme.AppCompat.Light.DarkActionBar">
        <item name="android:windowBackground">@drawable/splash</item>
        <!--If you are using revision 22.1 please use just windowNoTitle. Without android:-->
        <item name="windowNoTitle">true</item>
        <!--We will be using the toolbar so no need to show ActionBar-->
        <item name="windowActionBar">false</item>
        <!-- Set theme colors from https://aka.ms/material-colors -->
        <!-- colorPrimary is used for the default action bar background -->
        <item name="colorPrimary">#2196F3</item>
        <!-- colorPrimaryDark is used for the status bar -->
        <item name="colorPrimaryDark">#66BB6A</item>
        <!-- colorAccent is used as the default value for colorControlActivated
         which is used to tint widgets -->
        <item name="colorAccent">#FF4081</item>
        <!-- You can also set colorControlNormal, colorControlActivated
         colorControlHighlight and colorSwitchThumbNormal. -->
        <item name="windowActionModeOverlay">true</item>
        <item name="android:datePickerDialogTheme">@style/AppCompatDialogStyle</item>
    </style>
    <style name="AppCompatDialogStyle" parent="Theme.AppCompat.Light.Dialog">
        <item name="colorAccent">#FF4081</item>
    </style>
</resources>
```

これでアプリ起動時に緑のスプラッシュスクリーンが表示されるようになります。

### iOS の場合

iOS プロジェクトには、最初から ``LaunchScreen.storyboard`` がスプラッシュスクリーンとして使用されるよう設定されていますが、色が青になっているので、これを変更します。

``Resources/LaunchScreen.storyboard`` を開いて、``Background`` の色を ``#66BB6A``




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