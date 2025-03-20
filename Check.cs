using System;
using System.Drawing;
using OpenCvSharp;
using OpenCvSharp.Extensions;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Buffers;
using System.Reflection;

public class Check
{
    [DllImport("user32.dll")]
    static extern IntPtr GetDC(IntPtr hwnd);

    [DllImport("user32.dll")]
    static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

    [DllImport("gdi32.dll")]
    static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);
    public static System.Drawing.Point? FindTujen(double threshold = 0.8)
    {
        string resourceName = "DM_Tujen_2.tujenname.png"; // Ảnh mẫu cần tìm
        int width = 1280, height = 800;     // Kích thước vùng tìm kiếm

        // Xác định vùng cần chụp (bắt đầu từ góc trên bên trái màn hình)
        Rectangle captureRegion = new Rectangle(0, 0, width, height);

        // Chụp ảnh khu vực màn hình
        Bitmap screenshot = CaptureRegion(captureRegion);
        using Mat screenMat = BitmapConverter.ToMat(screenshot); // Chuyển Bitmap thành Mat

        // Đọc ảnh từ file nhúng trong resource
        Assembly assembly = Assembly.GetExecutingAssembly();
        using Stream? stream = assembly.GetManifestResourceStream(resourceName);
        if (stream == null) return null; // Nếu không tìm thấy tài nguyên, trả về false
        using Bitmap templateBitmap = new Bitmap(stream);
        using Mat template = BitmapConverter.ToMat(templateBitmap); // Chuyển Bitmap thành Mat

        // Chuyển ảnh màn hình về grayscale nếu chưa phải
        using Mat grayScreen = new Mat();
        Cv2.CvtColor(screenMat, grayScreen, ColorConversionCodes.BGR2GRAY);
        using Mat grayTemplate = new Mat();
        Cv2.CvtColor(template, grayTemplate, ColorConversionCodes.BGR2GRAY);

        // Thực hiện Template Matching
        using Mat result = new Mat();        
        Cv2.MatchTemplate(grayScreen, grayTemplate, result, TemplateMatchModes.CCoeffNormed);

        // Tìm vị trí có độ khớp cao nhất
        Cv2.MinMaxLoc(result, out _, out double maxVal, out _, out OpenCvSharp.Point maxLoc);

        // Nếu độ chính xác cao hơn ngưỡng threshold, trả về tọa độ trung tâm ảnh tìm thấy
        if (maxVal >= threshold)
        {
            int centerX = maxLoc.X + template.Width / 2;
            int centerY = maxLoc.Y + template.Height / 2;
            return new System.Drawing.Point(centerX, centerY);
        }

        return null; // Không tìm thấy hình ảnh
    }

    public static bool IsTujenOpen(double threshold = 0.8)
    {
        string templatePath = "DM_Tujen_2.tujenface.png"; // Ảnh mẫu cần tìm
        int width = 1280, height = 800;     // Kích thước vùng tìm kiếm

        // Xác định vùng cần chụp (bắt đầu từ góc trên bên trái màn hình)
        Rectangle captureRegion = new Rectangle(0, 0, width, height);

        // Chụp ảnh khu vực màn hình
        Bitmap screenshot = CaptureRegion(captureRegion);
        using Mat screenMat = BitmapConverter.ToMat(screenshot); // Chuyển Bitmap thành Mat
        //using Mat template = Cv2.ImRead(templatePath, ImreadModes.Grayscale); // Đọc ảnh mẫu dạng grayscale     

        // Đọc ảnh từ file nhúng trong resource
        Assembly assembly = Assembly.GetExecutingAssembly();
        using Stream? stream = assembly.GetManifestResourceStream(templatePath);
        if (stream == null) return false; // Nếu không tìm thấy tài nguyên, trả về false
        using Bitmap templateBitmap = new Bitmap(stream);
        using Mat template = BitmapConverter.ToMat(templateBitmap); // Chuyển Bitmap thành Mat

        // Chuyển ảnh màn hình về grayscale nếu chưa phải
        using Mat grayScreen = new Mat();
        Cv2.CvtColor(screenMat, grayScreen, ColorConversionCodes.BGR2GRAY);
        using Mat grayTemplate = new Mat();
        Cv2.CvtColor(template, grayTemplate, ColorConversionCodes.BGR2GRAY);


        // Thực hiện Template Matching
        using Mat result = new Mat();
        Cv2.MatchTemplate(grayScreen, grayTemplate, result, TemplateMatchModes.CCoeffNormed);

        // Tìm vị trí có độ khớp cao nhất
        Cv2.MinMaxLoc(result, out _, out double maxVal, out _, out OpenCvSharp.Point maxLoc);

        // Nếu độ chính xác cao hơn ngưỡng threshold, trả về tọa độ trung tâm ảnh tìm thấy
        if (maxVal >= threshold)
        {
            return true;
        }
        return false; // Không tìm thấy hình ảnh
    }

    public static System.Drawing.Point? FindStash(double threshold = 0.8)
    {
        string templatePath = "DM_Tujen_2.stash.png"; // Ảnh mẫu cần tìm
        int width = 1280, height = 800;     // Kích thước vùng tìm kiếm

        // Xác định vùng cần chụp (bắt đầu từ góc trên bên trái màn hình)
        Rectangle captureRegion = new Rectangle(0, 0, width, height);

        // Chụp ảnh khu vực màn hình
        Bitmap screenshot = CaptureRegion(captureRegion);
        using Mat screenMat = BitmapConverter.ToMat(screenshot); // Chuyển Bitmap thành Mat

        // Đọc ảnh từ file nhúng trong resource
        Assembly assembly = Assembly.GetExecutingAssembly();
        using Stream? stream = assembly.GetManifestResourceStream(templatePath);
        if (stream == null) return null; // Nếu không tìm thấy tài nguyên, trả về false
        using Bitmap templateBitmap = new Bitmap(stream);
        using Mat template = BitmapConverter.ToMat(templateBitmap); // Chuyển Bitmap thành Mat

        // Chuyển ảnh màn hình về grayscale nếu chưa phải
        using Mat grayScreen = new Mat();
        Cv2.CvtColor(screenMat, grayScreen, ColorConversionCodes.BGR2GRAY);
        using Mat grayTemplate = new Mat();
        Cv2.CvtColor(template, grayTemplate, ColorConversionCodes.BGR2GRAY);

        // Thực hiện Template Matching
        using Mat result = new Mat();
        Cv2.MatchTemplate(grayScreen, grayTemplate, result, TemplateMatchModes.CCoeffNormed);

        // Tìm vị trí có độ khớp cao nhất
        Cv2.MinMaxLoc(result, out _, out double maxVal, out _, out OpenCvSharp.Point maxLoc);

        // Nếu độ chính xác cao hơn ngưỡng threshold, trả về tọa độ trung tâm ảnh tìm thấy
        if (maxVal >= threshold)
        {
            int centerX = maxLoc.X + template.Width / 2;
            int centerY = maxLoc.Y + template.Height / 2;
            return new System.Drawing.Point(centerX, centerY);
        }
        return null; // Không tìm thấy hình ảnh
    }

    public static bool IsStashOpen(double threshold = 0.8)
    {
        string templatePath = "DM_Tujen_2.stashface.png"; // Ảnh mẫu cần tìm
        int width = 1280, height = 800;     // Kích thước vùng tìm kiếm

        // Xác định vùng cần chụp (bắt đầu từ góc trên bên trái màn hình)
        Rectangle captureRegion = new Rectangle(0, 0, width, height);

        // Chụp ảnh khu vực màn hình
        Bitmap screenshot = CaptureRegion(captureRegion);
        using Mat screenMat = BitmapConverter.ToMat(screenshot); // Chuyển Bitmap thành Mat

        // Đọc ảnh từ file nhúng trong resource
        Assembly assembly = Assembly.GetExecutingAssembly();
        using Stream? stream = assembly.GetManifestResourceStream(templatePath);
        if (stream == null) return false; // Nếu không tìm thấy tài nguyên, trả về false
        using Bitmap templateBitmap = new Bitmap(stream);
        using Mat template = BitmapConverter.ToMat(templateBitmap); // Chuyển Bitmap thành Mat

        // Chuyển ảnh màn hình về grayscale nếu chưa phải
        using Mat grayScreen = new Mat();
        Cv2.CvtColor(screenMat, grayScreen, ColorConversionCodes.BGR2GRAY);
        using Mat grayTemplate = new Mat();
        Cv2.CvtColor(template, grayTemplate, ColorConversionCodes.BGR2GRAY);

        // Thực hiện Template Matching
        using Mat result = new Mat();
        Cv2.MatchTemplate(grayScreen, grayTemplate, result, TemplateMatchModes.CCoeffNormed);

        // Tìm vị trí có độ khớp cao nhất
        Cv2.MinMaxLoc(result, out _, out double maxVal, out _, out OpenCvSharp.Point maxLoc);

        // Nếu độ chính xác cao hơn ngưỡng threshold, trả về tọa độ trung tâm ảnh tìm thấy
        if (maxVal >= threshold)
        {
            return true;
        }
        return false; // Không tìm thấy hình ảnh
    }

    public static List<System.Drawing.Point> Inventory2()
    {
        // Kích thước kho đồ
        int cols = 12, rows = 5; // 12x5 = 60 ô
        int slotWidth = 37, slotHeight = 37; // Mỗi ô là 37x37 pixel
        int startX = 815, startY = 445; // Vị trí góc trên bên trái kho đồ trên màn hình

        // Màu nền để so sánh (thay đổi nếu khác)
        Color backgroundColor = Color.FromArgb(4, 6, 6); // Ví dụ màu nền

        // Chụp ảnh kho đồ
        using Bitmap screenshot = new Bitmap(cols * slotWidth, rows * slotHeight);
        using Graphics g = Graphics.FromImage(screenshot);
        g.CopyFromScreen(startX, startY, 0, 0, screenshot.Size);

        // Dùng LockBits để truy cập pixel nhanh hơn
        BitmapData bmpData = screenshot.LockBits(new Rectangle(0, 0, screenshot.Width, screenshot.Height),
                                                 ImageLockMode.ReadOnly,
                                                 PixelFormat.Format24bppRgb);

        int stride = bmpData.Stride;
        int bytes = stride * screenshot.Height;
        byte[] pixelData = new byte[bytes];
        Marshal.Copy(bmpData.Scan0, pixelData, 0, bytes);
        screenshot.UnlockBits(bmpData);


        // Danh sách tọa độ trên màn hình của các ô chứa item
        List<System.Drawing.Point> itemPositions = new List<System.Drawing.Point>();

        // ✅ Dùng `Parallel.For` để kiểm tra đa luồng
        Parallel.For(0, rows, row =>
        {
            for (int col = 0; col < cols; col++)
            {
                int centerX = startX + col * slotWidth + slotWidth / 2; // Vị trí X trên màn hình
                int centerY = startY + row * slotHeight + slotHeight / 2; // Vị trí Y trên màn hình
                Color pixelColor = GetPixelFast(pixelData, (col * slotWidth) + slotWidth / 2,
                                                         (row * slotHeight) + slotHeight / 2, stride);

                // Nếu màu không giống nền, nghĩa là có item
                if (!IsSameColor(pixelColor, backgroundColor))
                {
                    lock (itemPositions) // Đảm bảo thread an toàn
                    {
                        itemPositions.Add(new System.Drawing.Point(centerX, centerY)); // Lưu tọa độ trên màn hình
                    }
                }
            }
        });
        itemPositions = itemPositions.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
        return itemPositions;
    }

    public static Span<System.Drawing.Point> Inventory3()
    {
        int cols = 12, rows = 5;
        int slotWidth = 37, slotHeight = 37;
        int startX = 815, startY = 445;
        Color backgroundColor = Color.FromArgb(4, 6, 6);

        using Bitmap screenshot = new Bitmap(cols * slotWidth, rows * slotHeight);
        using Graphics g = Graphics.FromImage(screenshot);
        g.CopyFromScreen(startX, startY, 0, 0, screenshot.Size);

        BitmapData bmpData = screenshot.LockBits(new Rectangle(0, 0, screenshot.Width, screenshot.Height),
                                                 ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);

        int stride = bmpData.Stride;
        int bytes = stride * screenshot.Height;
        byte[] pixelData = ArrayPool<byte>.Shared.Rent(bytes); // 🔥 Dùng ArrayPool để tránh cấp phát mới
        Marshal.Copy(bmpData.Scan0, pixelData, 0, bytes);
        screenshot.UnlockBits(bmpData);

        // 🔥 Dùng ArrayPool để tránh cấp phát List<>
        System.Drawing.Point[] itemBuffer = ArrayPool<System.Drawing.Point>.Shared.Rent(rows * cols);
        int itemCount = 0;

        Parallel.For(0, rows, row =>
        {
            for (int col = 0; col < cols; col++)
            {
                int centerX = startX + col * slotWidth + slotWidth / 2;
                int centerY = startY + row * slotHeight + slotHeight / 2;
                Color pixelColor = GetPixelFast(pixelData, (col * slotWidth) + slotWidth / 2,
                                                         (row * slotHeight) + slotHeight / 2, stride);

                if (!IsSameColor(pixelColor, backgroundColor))
                {
                    int index = Interlocked.Increment(ref itemCount) - 1;
                    itemBuffer[index] = new System.Drawing.Point(centerX, centerY);
                }
            }
        });

        ArrayPool<byte>.Shared.Return(pixelData); // Trả bộ nhớ về pool

        // 🔥 Sắp xếp từ trên xuống dưới, từ trái qua phải
        Array.Sort(itemBuffer, 0, itemCount, Comparer<System.Drawing.Point>.Create((p1, p2) =>
        {
            int cmpX = p1.X.CompareTo(p2.X); // So sánh hàng trước
            return cmpX != 0 ? cmpX : p1.Y.CompareTo(p2.Y); // Nếu cùng hàng, so sánh cột
        }));

        return new Span<System.Drawing.Point>(itemBuffer, 0, itemCount);
    }

    public static List<System.Drawing.Point> TujenItemPos()
    {
        // Kích thước kho đồ
        int cols = 2, rows = 11; // 12x5 = 60 ô
        int slotWidth = 37, slotHeight = 37; // Mỗi ô là 37x37 pixel
        int startX = 183, startY = 216; // Vị trí góc trên bên trái kho đồ trên màn hình

        // Màu nền để so sánh (thay đổi nếu khác)
        Color backgroundColor = Color.FromArgb(4, 6, 6); // Ví dụ màu nền

        // Chụp ảnh kho đồ
        using Bitmap screenshot = new Bitmap(cols * slotWidth, rows * slotHeight);
        using Graphics g = Graphics.FromImage(screenshot);
        g.CopyFromScreen(startX, startY, 0, 0, screenshot.Size);

        // Dùng LockBits để truy cập pixel nhanh hơn
        BitmapData bmpData = screenshot.LockBits(new Rectangle(0, 0, screenshot.Width, screenshot.Height),
                                                 ImageLockMode.ReadOnly,
                                                 PixelFormat.Format24bppRgb);

        int stride = bmpData.Stride;
        int bytes = stride * screenshot.Height;
        byte[] pixelData = new byte[bytes];
        Marshal.Copy(bmpData.Scan0, pixelData, 0, bytes);
        screenshot.UnlockBits(bmpData);


        // Danh sách tọa độ trên màn hình của các ô chứa item
        List<System.Drawing.Point> itemPositions = new List<System.Drawing.Point>();

        // ✅ Dùng `Parallel.For` để kiểm tra đa luồng
        Parallel.For(0, rows, row =>
        {
            for (int col = 0; col < cols; col++)
            {
                int centerX = startX + col * slotWidth + slotWidth / 2; // Vị trí X trên màn hình
                int centerY = startY + row * slotHeight + slotHeight / 2; // Vị trí Y trên màn hình
                Color pixelColor = GetPixelFast(pixelData, (col * slotWidth) + slotWidth / 2,
                                                         (row * slotHeight) + slotHeight / 2, stride);

                // Nếu màu không giống nền, nghĩa là có item
                if (!IsSameColor(pixelColor, backgroundColor))
                {
                    lock (itemPositions) // Đảm bảo thread an toàn
                    {
                        itemPositions.Add(new System.Drawing.Point(centerX, centerY)); // Lưu tọa độ trên màn hình
                    }
                }
            }
        });
        itemPositions = itemPositions.OrderBy(p => p.X).ThenBy(p => p.Y).ToList();
        return itemPositions;
    }

    public static bool isConfirmOn()
    {
        IntPtr hdc = GetDC(IntPtr.Zero);

        try
        {
            Color color1 = GetPixelColor(hdc, 312, 341);
            Color color2 = GetPixelColor(hdc, 490, 341);

            Color expectedColor1 = Color.FromArgb(29, 28, 28);
            Color expectedColor2 = Color.FromArgb(28, 26, 26);

            return (color1 == expectedColor1 && color2 == expectedColor2);
        }
        finally
        {
            ReleaseDC(IntPtr.Zero, hdc);
        }
    }

    public static bool isOutOfArtf()
    {
        IntPtr hdc = GetDC(IntPtr.Zero);

        try
        {
            Color color1 = GetPixelColor(hdc, 358, 650);
            Color color2 = GetPixelColor(hdc, 442, 650);

            Color expectedColor1 = Color.FromArgb(66, 66, 66);
            Color expectedColor2 = Color.FromArgb(67, 67, 67);

            return (color1 == expectedColor1 && color2 == expectedColor2);
        }
        finally
        {
            ReleaseDC(IntPtr.Zero, hdc);
        }
    }

    public static bool isOutOfCoin()
    {
        IntPtr hdc = GetDC(IntPtr.Zero);

        try
        {
            Color color1 = GetPixelColor(hdc, 619, 660);
            Color color2 = GetPixelColor(hdc, 641, 660);

            Color expectedColor1 = Color.FromArgb(51, 51, 51);
            Color expectedColor2 = Color.FromArgb(47, 47, 47);

            return (color1 == expectedColor1 && color2 == expectedColor2);
        }
        finally
        {
            ReleaseDC(IntPtr.Zero, hdc);
        }
    }

    public static bool IsItemWhitelisted(string itemName)
    {
        return Whitelist.list.Contains(itemName);
    }

    public static Bitmap CaptureRegion(Rectangle region)
    {
        Bitmap bitmap = new Bitmap(region.Width, region.Height);
        using (Graphics g = Graphics.FromImage(bitmap))
        {
            g.CopyFromScreen(region.Location, System.Drawing.Point.Empty, region.Size);
        }
        return bitmap;
    }

    private static Color GetPixelColor(IntPtr hdc, int x, int y)
    {
        uint pixel = GetPixel(hdc, x, y);
        int r = (int)(pixel & 0x000000FF);
        int g = (int)((pixel & 0x0000FF00) >> 8);
        int b = (int)((pixel & 0x00FF0000) >> 16);
        return Color.FromArgb(r, g, b);
    }

    // 🛠 Lấy màu từ byte array của Bitmap (tăng tốc độ so với GetPixel)
    static Color GetPixelFast(byte[] pixelData, int x, int y, int stride)
    {
        int index = (y * stride) + (x * 3);
        return Color.FromArgb(pixelData[index + 2], pixelData[index + 1], pixelData[index]);
    }

    // 🔍 So sánh màu có giống nhau không (cho phép sai số nhỏ)
    static bool IsSameColor(Color a, Color b, int tolerance = 10)
    {
        return Math.Abs(a.R - b.R) < tolerance &&
               Math.Abs(a.G - b.G) < tolerance &&
               Math.Abs(a.B - b.B) < tolerance;
    }
}
