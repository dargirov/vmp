function isDescendant(parent, child) {
    if (parent == child) {
        return true;
    }

    var node = child.parentNode;
    while (node != null) {
        if (node == parent) {
            return true;
        }

        node = node.parentNode;
    }

    return false;
}

function isMobile() {
    return window.innerWidth <= 1000;
}