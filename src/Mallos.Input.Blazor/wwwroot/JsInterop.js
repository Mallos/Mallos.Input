
let _callbackInstance = null;

export function init(instance) {
  if (_callbackInstance) {
    return false;
  }

  _callbackInstance = instance;
  return true;
}

export function getBoundingClientRect(id) {
  const element = document.getElementById(id);
  if (element) {
    return element.getBoundingClientRect();
  }
  return null;
}

export function addEvents(id) {
  if (_callbackInstance === null) {
    return false;
  }

  const canvas = document.querySelector(`[id='${id}'] canvas`);
  if (typeof canvas === 'undefined') {
    return false;
  }

  console.log(canvas);

  ///
  /// Mouse
  ///

  canvas.addEventListener('mousemove', e => {
    const clientRect = e.target.getBoundingClientRect();
    const absPositionX = e.clientX - clientRect.x;
    const absPositionY = e.clientY - clientRect.y;
    _callbackInstance.invokeMethodAsync('OnMouseMove', absPositionX, absPositionY);
  });

  canvas.addEventListener('mousedown', e => {
    _callbackInstance.invokeMethodAsync('OnMouseDown', e.button);
  });

  canvas.addEventListener('mouseup', e => {
    _callbackInstance.invokeMethodAsync('OnMouseUp', e.button);
  });

  canvas.addEventListener('mousewheel', e => {
    _callbackInstance.invokeMethodAsync('OnMouseWheel', e.deltaY);
  });

  ///
  /// Keyboard
  ///

  canvas.addEventListener('keydown', e => {
    _callbackInstance.invokeMethodAsync('OnKeyDown', e.keyCode);
  });

  canvas.addEventListener('keyup', e => {
    _callbackInstance.invokeMethodAsync('OnKeyUp', e.keyCode);
  });

  ///
  /// Touch
  ///

  function invokeTouchEvent(e) {
    const touchPoints = [];
    for (let i = 0; i < e.touches.length; i++) {
      const item = e.touches[i];
      touchPoints.push({
        Identifier: item.identifier,
        X: item.pageX,
        Y: item.pageY,
        Force: item.force,
        Radius: Math.max(item.radiusX, item.radiusY),
      });
    }

    _callbackInstance.invokeMethodAsync('OnTouch', touchPoints);
  }

  canvas.addEventListener('touchstart', e => {
    invokeTouchEvent(e);
  });

  canvas.addEventListener('touchend', e => {
    invokeTouchEvent(e);
  });

  canvas.addEventListener('touchmove', e => {
    invokeTouchEvent(e);
  });

  canvas.addEventListener('touchenter', e => {
    invokeTouchEvent(e);
  });

  canvas.addEventListener('touchleave', e => {
    invokeTouchEvent(e);
  });

  canvas.addEventListener('touchcancel', e => {
    invokeTouchEvent(e);
  });

  return true;
}
