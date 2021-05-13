# Mallos.Input.Blazor
Mallos.Input for Blazor canvas.

> This is experimental

## Getting Started

Hook the Javascript events:
```js
// \wwwroot\index.html

// Mouse
window.game.canvas.onmousemove = (e) => {
    const clientRect = e.target.getBoundingClientRect();
    const absPositionX = e.clientX - clientRect.x;
    const absPositionY = e.clientY - clientRect.y;
    game.instance.invokeMethodAsync('OnMouseMove', absPositionX, absPositionY);
};
window.game.canvas.onmousedown = (e) => {
    game.instance.invokeMethodAsync('OnMouseDown', e.button);
};
window.game.canvas.onmouseup = (e) => {
    game.instance.invokeMethodAsync('OnMouseUp', e.button);
};
window.game.canvas.onmousewheel = (e) => {
    game.instance.invokeMethodAsync('OnMouseWheel', e.deltaY);
};

// Keyboard
window.game.canvas.onkeydown = (e) => {
    game.instance.invokeMethodAsync('OnKeyDown', e.keyCode);
};
window.game.canvas.onkeyup = (e) => {
    game.instance.invokeMethodAsync('OnKeyUp', e.keyCode);
};

// Touch
function invokeTouchEvent(e) {
    const touchPoints = [];
    for (let i = 0; i < e.touches.length; i++) {
        const item = e.touches[i];
        touchPoints.push({
            identifier: item.identifier,
            x: item.pageX,
            y: item.pageY,
            force: item.force,
            radius: Math.max(item.radiusX, item.radiusY),
        });
    }

    game.instance.invokeMethodAsync('OnTouch', touchPoints);
}

window.game.canvas.ontouchstart = (e) => {
    invokeTouchEvent(e);
};
window.game.canvas.ontouchend = (e) => {
    invokeTouchEvent(e);
};
window.game.canvas.ontouchmove = (e) => {
    invokeTouchEvent(e);
};
window.game.canvas.ontouchenter = (e) => {
    invokeTouchEvent(e);
};
window.game.canvas.ontouchleave = (e) => {
    invokeTouchEvent(e);
};
window.game.canvas.ontouchcancel = (e) => {
    invokeTouchEvent(e);
};
```

Hook the CSharp Blazor events:
```cs
// \Pages\Index.razor
[JSInvokable]
public ValueTask OnMouseMove(int mouseX, int mouseY)
    => BlazorMouseState.OnMouseMove(mouseX, mouseY);

[JSInvokable]
public ValueTask OnMouseWheel(int deltaMode)
    => BlazorMouseState.OnMouseWheel(deltaMode);

[JSInvokable]
public ValueTask OnMouseDown(int buttons)
    => BlazorMouseState.OnMouseToggle(buttons, true);

[JSInvokable]
public ValueTask OnMouseUp(int buttons)
    => BlazorMouseState.OnMouseToggle(buttons, false);

[JSInvokable]
public ValueTask OnKeyDown(int keyCode)
    => BlazorKeyboardState.OnKeyDown(keyCode);

[JSInvokable]
public ValueTask OnKeyUp(int keyCode)
    => BlazorKeyboardState.OnKeyUp(keyCode);

[JSInvokable]
public ValueTask OnTouch(BlazorTouchPoint[] points)
    => BlazorTouchDeviceState.OnTouch(points);
```
