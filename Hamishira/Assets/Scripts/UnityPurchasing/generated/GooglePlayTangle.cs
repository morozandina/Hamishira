// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("a6vSbbvB9RhRZHOvpVb3AIw3qr7vXd7979LZ1vVZl1ko0t7e3trf3Lm2KIGHe0kcxqQLoXNLp2zV9vRtRaCdAGgEpmOJ4hGlvWJjl4K3+tflF90EzSr6yvd2NrWpOpVdYZOnV01ZFzqW7MzCJqXIsG2bHodfkL21AjQChzUeFApwx9ZY96kRvv6Y4Q1d3tDf713e1d1d3t7fdKRWenX57Yn/17pKkbAi8rLO+gI2KSAIHw/2fIV1I7SprEYsR2gqH7Rh9qt3SXJg8lbNNS5vQJ5i5ixvOE4QILDXEhZq45/CoT1g9CYhPn9FNzbSGe/l1VBhf4uvN612oFfwPSyGNvne85MadghJ4729XQwfC072bfTQU9N2s8oU+btojwEEqt3c3t/e");
        private static int[] order = new int[] { 3,3,7,7,9,13,13,7,9,13,12,12,13,13,14 };
        private static int key = 223;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
