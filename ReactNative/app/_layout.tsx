import { ThemeProvider } from "@/hooks/useTheme";
import { Stack } from "expo-router";
import { ConvexProvider, ConvexReactClient } from "convex/react";

const convex = new ConvexReactClient(process.env.EXPO_PUBLIC_CONVEX_URL!, {
unsavedChangesWarning: false,
});
export default function RootLayout() {
  return (
    // 화면위에쌓이는방식..가로로 options로 이름바꾸고
    <ConvexProvider client={convex}>
      <ThemeProvider>
        <Stack screenOptions={{ headerShown: false }}>
          <Stack.Screen
            name="(tabs)"
            // options={{ title: "Home" }}
          />
          {/* <Stack.Screen name="about" options={{ title: "About" }} /> */}
        </Stack>
      </ThemeProvider>
    </ConvexProvider>
  );
}
