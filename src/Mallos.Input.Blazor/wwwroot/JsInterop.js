
export function getBoundingClientRect(id) {
  const element = document.getElementById(id);
  if (element) {
    return element.getBoundingClientRect();
  }
  return null;
}
